using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegraPartnersContactApplicationAPI;
using IntegraPartnersContactApplicationAPI.Model;
using IntegraPartnersContactApplicationAPI.Interface;
using IntegraPartnersContactApplicationAPI.ViewModel;
using IntegraPartnersContactApplicationAPI.mapping;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;

namespace IntegraPartnersContactApplicationAPI.Controllers
{
    public class UsersController : Controller, IUsersController
    {
        private readonly IUsersRepository _IUsersRepository;
        private readonly IMapping _mapper;

        public UsersController(IUsersRepository IUsersRepository, IMapping mapper)
        {
            _IUsersRepository = IUsersRepository;
            _mapper = mapper;
        }

        // GET: Users/GetAllUsers

    
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var listOfAllUsers = new List<UsersViewModel>();
            var result = await _IUsersRepository.GetAllUsers();
            result.ForEach(x =>
            {
                listOfAllUsers.Add(_mapper.MapEntityToViewModel(x));
            });
            return new JsonResult(listOfAllUsers);
        }

        // GET: Users/GetUser/5
        [HttpGet]
        public async Task<JsonResult> GetUserByID(int id)
        {

            var userViewModel = _mapper.MapEntityToViewModel(await _IUsersRepository.GetUserByID(id));
            if (userViewModel == null)
            {

                return new JsonResult(new Exception("Could Not Find User With Specified ID").ToJson());
            }

            return new  JsonResult(userViewModel);
        }


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateUser([Bind("UserID,Username,FirstName,LastName,Email,UserStatus,Department")] UsersViewModel userViewModel)
        {
            
            if (ModelState.IsValid)
            {
               if(UserExists(userViewModel.UserID) == false)
                {
                    var userEntity = _mapper.MapViewModelToEntity(userViewModel);
                    var result = await _IUsersRepository.CreateUser(userEntity);
                    return new JsonResult(result);
                }
                else
                {
                    return new JsonResult(new Exception("the user that was attempted to be created already exist in the database").ToJson());
                }
            }
            return new JsonResult(new Exception("error occured while trying to create a user. Please check to make sure all value are accurate").ToJson());
        }

        // GET: Users/FindUser/5
        [HttpGet]
        public async Task<JsonResult> FindUser(int id)
        {
            var userViewModel = _mapper.MapEntityToViewModel(await _IUsersRepository.FindUser(id));
            if (userViewModel == null)
            {
                return new JsonResult(new Exception("Could Not Find User With The Submitted ID.").ToJson());
            }
            return new JsonResult(userViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditUser(int id, [Bind("UserID,Username,FirstName,LastName,Email,UserStatus,Department")] UsersViewModel userViewModel)
        {
            if (id != userViewModel.UserID)
            {
                return new JsonResult(new Exception("the id sent with the request does not match the id in the user object").ToJson());
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(UserExists(id))
                    {
                        var userEntity = _mapper.MapViewModelToEntity(userViewModel);
                        var result = await _IUsersRepository.EditUser(id, userEntity);
                        return Json(result);
                    }
                    else
                    {
                        return new JsonResult(new Exception("the user being edited is not found in the database").ToJson());
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.UserID))
                    {
                        return new JsonResult(new Exception("the user being edited is not found in the database").ToJson());
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return new JsonResult(new Exception("Something went wrong. Please check all data from the request and make sure it is valid.").ToJson());
        }

        // POST: Users/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteUser(int id)
        {
            var user = await _IUsersRepository.FindUser(id);
            if (user != null)
            {
               await _IUsersRepository.DeleteUser(id);
                return Json(1);
            }
            else
            {
                return new JsonResult(new Exception("could not find the user to delete.").ToJson());
            }
        }

        public bool UserExists(int id)
        {
            return _IUsersRepository.UserExists(id);
        }
    }
}
