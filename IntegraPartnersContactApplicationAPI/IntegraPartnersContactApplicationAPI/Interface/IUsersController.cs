using IntegraPartnersContactApplicationAPI.Model;
using IntegraPartnersContactApplicationAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IntegraPartnersContactApplicationAPI.Interface
{
    public interface IUsersController
    {
        Task<JsonResult> GetAllUsers();
        Task<JsonResult> GetUserByID(int ? id);
        Task<JsonResult> CreateUser([Bind("UserID,Username,FirstName,LastName,Email,UserStatus,Department")] UserViewModel userViewModel);
        Task<JsonResult> FindUser(int ? id);
        Task<JsonResult> EditUser(int ? id, [Bind("UserID,Username,FirstName,LastName,Email,UserStatus,Department")] UserViewModel userViewModel);
        Task<JsonResult> DeleteUser(int ? id);
        bool UserExists(int ? id);
    }
}
