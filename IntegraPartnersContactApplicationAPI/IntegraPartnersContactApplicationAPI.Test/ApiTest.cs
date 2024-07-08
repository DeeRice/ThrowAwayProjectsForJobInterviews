using IntegraPartnersContactApplicationAPI.Controllers;
using IntegraPartnersContactApplicationAPI.Interface;
using IntegraPartnersContactApplicationAPI.Model;
using IntegraPartnersContactApplicationAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntegraPartnersContactApplicationAPI.Test
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public void GetAllUsersReturnsAnEmptyList()
        {
            // Arrange test
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            iMockUserRepository.Setup(x => x.GetAllUsers()).Returns(Task.FromResult(new List<Users>()));
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.GetAllUsers();

            }).Wait();
            List<UsersViewModel> okResult = JsonConvert.DeserializeObject<List<UsersViewModel>>(result.Value.ToJson());

            // Assert test
            Assert.AreEqual(okResult?.Count, 0);
        }

        
        [TestMethod]
        public void GetAllUsersReturnsAListUsers()
        {     // Arrange test
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            var user = new Users() { user_id = 1, first_name = "john", last_name = "doe" };
            var userViewModel = new UsersViewModel() { UserID = 1, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            iMockUserRepository.Setup(x => x.GetAllUsers()).Returns(Task.FromResult(list));
            iMapper.Setup(x => x.MapEntityToViewModel(user)).Returns(userViewModel);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult ? result = null;

            // Act test
            Task.Run(async () =>
                {
                  
                    result = await controller.GetAllUsers();
                   
                }).Wait();
            List<UsersViewModel> okResult = JsonConvert.DeserializeObject<List<UsersViewModel>>(result.Value.ToJson());
           
            // Assert test
            Assert.AreEqual(okResult?.Count, 1);
        

        }

        [TestMethod]
        public void GetUserByIDReturnsAUser()
        {
            // Arrange test
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            var user = new Users() { user_id = 1, first_name = "john", last_name = "doe" };
            var userViewModel = new UsersViewModel() { UserID = 1, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            iMockUserRepository.Setup(x => x.GetUserByID(user.user_id)).Returns(Task.FromResult(user));
            iMapper.Setup(x => x.MapEntityToViewModel(user)).Returns(userViewModel);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.GetUserByID(userViewModel.UserID);

            }).Wait();
            UsersViewModel okResult = JsonConvert.DeserializeObject<UsersViewModel>(result.Value.ToJson());

            // Assert test
            Assert.AreEqual(okResult.ToJson(), userViewModel.ToJson());
        }

        [TestMethod]
        public void GetUserByIDReturnsAUserNotFoundMessage()
        {
            // Arrange test


            // Act test

            // Asset test
        }

        [TestMethod]
        public void EditUserReturnsAUserNotFoundMessage()
        {
            // Arrange test


            // Act test

            // Asset test
        }

        [TestMethod]
        public void EditUserReturnsAOneForSuccess()
        {
            // Arrange test


            // Act test

            // Asset test
        }

        [TestMethod]
        public void CreateUserReturnsADuplicateUserMessage()
        {
            // Arrange test


            // Act test

            // Asset test
        }

        [TestMethod]
        public void CreateUserReturnsAOneForSuccess()
        {
            // Arrange test


            // Act test

            // Asset test
        }

        [TestMethod]
        public void DeleteUserReturnsAOneForSuccess()
        {
            // Arrange test


            // Act test

            // Asset test
        }

        [TestMethod]
        public void DeleteUserReturnsAUserNotFound()
        {
            // Arrange test


            // Act test

            // Asset test
        }
    }
}