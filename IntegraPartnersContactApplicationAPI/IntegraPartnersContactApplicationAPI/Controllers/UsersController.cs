using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegraPartnersContactApplicationAPI;
using IntegraPartnersContactApplicationAPI.ViewModel;

namespace IntegraPartnersContactApplicationAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IntegraPartnersContactAPIDataContext _context;

        public UsersController(IntegraPartnersContactAPIDataContext context)
        {
            _context = context;
        }

        // GET: Users/GetAllUsers

    
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _context.Users.ToListAsync();

            return Json(result);
        }

        // GET: Users/GetUser/5
        [HttpGet]
        public async Task<IActionResult> GetUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.user_id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("UserID,Username,FirstName,LastName,Email,UserStatus,Department")] Users user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Json(1);
            }
            return Json(-1);
        }

        // GET: Users/FindUser/5
        [HttpGet]
        public async Task<IActionResult> FindUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Json(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("UserID,Username,FirstName,LastName,Email,UserStatus,Department")] Users users)
        {
            if (id != users.user_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(users.user_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(1);
            }
            return Json(-1);
        }

        // POST: Users/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Json(1);
            }


            return Json(-1);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.user_id == id);
        }
    }
}
