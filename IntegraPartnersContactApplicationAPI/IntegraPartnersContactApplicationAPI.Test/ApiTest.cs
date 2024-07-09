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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            Users user = null;
            string exceptionMessage = "Could Not Find User With Specified ID";
            UsersViewModel userViewModel = null;
            List<Users> list = new List<Users>();
            list.Add(user);
            iMockUserRepository.Setup(x => x.GetUserByID(0)).Returns(Task.FromResult(user));
            iMapper.Setup(x => x.MapEntityToViewModel(user)).Returns(userViewModel);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.GetUserByID(0);

            }).Wait();
            string okResult = JsonConvert.DeserializeObject<string>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, exceptionMessage);
        }

        [TestMethod]
        public void EditUserReturnsAUserNotSubmittedMessage()
        {
            // Arrange test
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            Users user = null;
            string exceptionMessage = "a user was not submitted to be updated.";
            UsersViewModel userViewModel = null;
            List<Users> list = new List<Users>();
            list.Add(user);
            iMockUserRepository.Setup(x => x.EditUser(0, user)).Returns(Task.FromResult(-1));
            iMapper.Setup(x => x.MapEntityToViewModel(user)).Returns(userViewModel);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.EditUser(0, userViewModel);

            }).Wait();
            string okResult = JsonConvert.DeserializeObject<string>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, exceptionMessage);
        }

        [TestMethod]
        public void EditUserReturnsAUserNotFoundMessage()
        {
            // Arrange test
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            Users user = null;
            string exceptionMessage = "the user being edited is not found in the database";
            var userViewModel = new UsersViewModel() { UserID = 1, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            iMockUserRepository.Setup(x => x.EditUser(0, user)).Returns(Task.FromResult(-1));
            iMapper.Setup(x => x.MapEntityToViewModel(user)).Returns(userViewModel);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.EditUser(1, userViewModel);

            }).Wait();
            string okResult = JsonConvert.DeserializeObject<string>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, exceptionMessage);
        }

        [TestMethod]
        public void EditUserReturnsMisMatchIDMessage()
        {
            // Arrange test
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            Users user = null;
            string exceptionMessage = "the id sent with the request does not match the id in the user object";
            var userViewModel = new UsersViewModel() { UserID = 0, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            iMockUserRepository.Setup(x => x.EditUser(0, user)).Returns(Task.FromResult(-1));
            iMapper.Setup(x => x.MapEntityToViewModel(user)).Returns(userViewModel);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.EditUser(1, userViewModel);

            }).Wait();
            string okResult = JsonConvert.DeserializeObject<string>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, exceptionMessage);
        }

        [TestMethod]
        public void EditUserReturnsAOneForSuccess()
        {
            // Arrange test
            var iMockUserRepository = new Mock<IUsersRepository>();
            var iMapper = new Mock<IMapping>();
            Users user = new Users() { user_id = 1, first_name = "john", last_name = "doe" };
            var userViewModel = new UsersViewModel() { UserID = 1, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            iMockUserRepository.Setup(x => x.UserExists(userViewModel.UserID)).Returns(true);
            iMockUserRepository.Setup(x => x.EditUser(1, user)).Returns(Task.FromResult(1));
            iMapper.Setup(x => x.MapViewModelToEntity(userViewModel)).Returns(user);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.EditUser(userViewModel.UserID, userViewModel);

            }).Wait();
            bool okResult = JsonConvert.DeserializeObject<bool>(result.Value.ToJson());

            // Assert test
            Assert.AreEqual(okResult, true);
        }

        [TestMethod]
        public void CreateUserReturnsADuplicateUserMessage()
        {
            // Arrange test
            Users user = new Users() { user_id = 0, first_name = "john", last_name = "doe" };
            var userViewModel = new UsersViewModel() { UserID = 0, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            var query = GetQueryableMockDbSet<Users>(list);
            var iMapper = new Mock<IMapping>();
            var iMockUserRepository = new Mock<IUsersRepository>();
            iMockUserRepository.Setup(x => x.PopulateDataSet(query)).Returns(query);
            string exceptionMessage = "the user that was attempted to be created already exist in the database";
            iMockUserRepository.Setup(x => x.CreateUser(user)).Returns(Task.FromResult(-1));
            iMockUserRepository.Setup(x => x.UserExists(user.user_id)).Returns(true);
            iMapper.Setup(x => x.MapViewModelToEntity(userViewModel)).Returns(user);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.CreateUser(userViewModel);

            }).Wait();
            string okResult = JsonConvert.DeserializeObject<string>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, exceptionMessage);
        }

        [TestMethod]
        public void CreateUserReturnsAOneForSuccess()
        {
            // Arrange test
            Users user = new Users() { user_id = 1, first_name = "john", last_name = "doe" };
            var userViewModel = new UsersViewModel() { UserID = 0, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            var query = GetQueryableMockDbSet<Users>(list);
            var iMapper = new Mock<IMapping>();
            var iMockUserRepository = new Mock<IUsersRepository>();
            iMockUserRepository.Setup(x => x.PopulateDataSet(query)).Returns(query);
            string exceptionMessage = "the user that was attempted to be created already exist in the database";
            iMockUserRepository.Setup(x => x.CreateUser(user)).Returns(Task.FromResult(1));
            iMockUserRepository.Setup(x => x.UserExists(user.user_id)).Returns(false);
            iMapper.Setup(x => x.MapViewModelToEntity(userViewModel)).Returns(user);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.CreateUser(userViewModel);

            }).Wait();
            bool okResult = JsonConvert.DeserializeObject<bool>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, true);
        }

        [TestMethod]
        public void DeleteUserReturnsAOneForSuccess()
        {
            // Arrange test
            Users user = new Users() { user_id = 0, first_name = "john", last_name = "doe" };
            var userViewModel = new UsersViewModel() { UserID = 0, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            var query = GetQueryableMockDbSet<Users>(list);
            var iMapper = new Mock<IMapping>();
            var iMockUserRepository = new Mock<IUsersRepository>();
            iMockUserRepository.Setup(x => x.PopulateDataSet(query)).Returns(query);
            string exceptionMessage = "the user that was attempted to be created already exist in the database";
            iMockUserRepository.Setup(x => x.DeleteUser(user.user_id)).Returns(Task.FromResult(1));
            iMockUserRepository.Setup(x => x.UserExists(user.user_id)).Returns(true);
            iMockUserRepository.Setup(x => x.FindUser(user.user_id)).Returns(Task.FromResult(user));
            iMapper.Setup(x => x.MapViewModelToEntity(userViewModel)).Returns(user);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.DeleteUser(userViewModel.UserID);

            }).Wait();
            bool okResult = JsonConvert.DeserializeObject<bool>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, true);
        }

        [TestMethod]
        public void DeleteUserReturnsAUserNotFound()
        {
            // Arrange test
            Users user = new Users() { user_id = 1, first_name = "john", last_name = "doe" };
            var userViewModel = new UsersViewModel() { UserID = 0, FirstName = "john", LastName = "doe" };
            List<Users> list = new List<Users>();
            list.Add(user);
            var query = GetQueryableMockDbSet<Users>(list);
            var iMapper = new Mock<IMapping>();
            var iMockUserRepository = new Mock<IUsersRepository>();
            iMockUserRepository.Setup(x => x.PopulateDataSet(query)).Returns(query);
            string exceptionMessage = "could not find the user to delete.";
            iMockUserRepository.Setup(x => x.DeleteUser(user.user_id)).Returns(Task.FromResult(1));
            iMockUserRepository.Setup(x => x.UserExists(user.user_id)).Returns(true);
            iMockUserRepository.Setup(x => x.FindUser(user.user_id)).Returns(Task.FromResult(user));
            iMapper.Setup(x => x.MapViewModelToEntity(userViewModel)).Returns(user);
            var controller = new UsersController(iMockUserRepository.Object, iMapper.Object);
            JsonResult? result = null;

            // Act test
            Task.Run(async () =>
            {

                result = await controller.DeleteUser(userViewModel.UserID);

            }).Wait();
            string okResult = JsonConvert.DeserializeObject<string>(result.Value.ToString());

            // Assert test
            Assert.AreEqual(okResult, exceptionMessage);
        }

        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}