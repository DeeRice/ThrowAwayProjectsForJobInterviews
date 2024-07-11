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
            var listOfAllUsers = new List<UserViewModel>();
            var result = await _IUsersRepository.GetAllUsers();
            result.ForEach(x =>
            {
                listOfAllUsers.Add(_mapper.MapEntityToViewModel(x));
            });
            return new JsonResult(listOfAllUsers);
        }

        // GET: Users/GetUser/5
        [HttpGet]
        public async Task<JsonResult> GetUserByID(int ? UserID)
        {

            var userViewModel = _mapper.MapEntityToViewModel(await _IUsersRepository.GetUserByID(UserID));
            if (userViewModel == null)
            {

                return new JsonResult(new Exception("Could Not Find User With Specified ID").Message.ToJson());
            }

            return new  JsonResult(userViewModel);
        }


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<JsonResult> CreateUser([FromBody] UserViewModel userViewModel)
        {
            
            if (ModelState.IsValid)
            {
               if(UserExists(userViewModel.UserID) == false)
                {
                    var userEntity = _mapper.MapViewModelToEntity(userViewModel);
                    var returnedViewModel = _mapper.MapEntityToViewModel(await _IUsersRepository.CreateUser(userEntity));
                    return new JsonResult(returnedViewModel);
                }
                else
                {
                    return new JsonResult(new Exception("the user that was attempted to be created already exist in the database").Message.ToJson());
                }
            }
            return new JsonResult(new Exception("error occurred while trying to create a user. Please check to make sure all value are accurate").Message.ToJson());
        }

        // GET: Users/FindUser/5
        [HttpGet]
        public async Task<JsonResult> FindUser(int ? UserID)
        {
            var userViewModel = _mapper.MapEntityToViewModel(await _IUsersRepository.FindUser(UserID));
            if (userViewModel == null)
            {
                return new JsonResult(new Exception("Could Not Find User With The Submitted ID.").Message.ToJson());
            }
            return new JsonResult(userViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        
        public async Task<JsonResult> EditUser(int ? UserID,[FromBody] UserViewModel userViewModel)
        {
            if(userViewModel == null)
            {
                return new JsonResult(new Exception("a user was not submitted to be updated.").Message.ToJson());
            }
            if (UserID != userViewModel.UserID)
            {
                return new JsonResult(new Exception("the id sent with the request does not match the id in the user object").Message.ToJson());
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(UserExists(UserID))
                    {
                        var userEntity = _mapper.MapViewModelToEntity(userViewModel);
                        var returnedViewModel = _mapper.MapEntityToViewModel(await _IUsersRepository.EditUser(UserID,userEntity));
                        return new JsonResult(returnedViewModel);
                    }
                    else
                    {
                        return new JsonResult(new Exception("the user being edited is not found in the database").Message.ToJson());
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.UserID))
                    {
                        return new JsonResult(new Exception("the user being edited is not found in the database").Message.ToJson());
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return new JsonResult(new Exception("Something went wrong. Please check all data from the request and make sure it is valid.").Message.ToJson());
        }

        // POST: Users/Delete/5
        [HttpDelete]
       
        public async Task<JsonResult> DeleteUser(int ? UserID)
        {
            var user = await _IUsersRepository.FindUser(UserID);
            if (user != null)
            {
                var returnedViewModel = _mapper.MapEntityToViewModel(await _IUsersRepository.DeleteUser(UserID));
                return new JsonResult(returnedViewModel);
            }
            else
            {
                return new JsonResult(new Exception("could not find the user to delete.").Message.ToJson());
            }
        }

        public bool UserExists(int ? UserID)
        {
            return _IUsersRepository.UserExists(UserID);
        }
    }
}
