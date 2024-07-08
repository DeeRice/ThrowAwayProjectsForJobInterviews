using IntegraPartnersContactApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace IntegraPartnersContactApplicationAPI.Interface
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserByID(int user_id);
        Task<int> CreateUser(Users user);
        Task<Users> FindUser(int user_id);
        Task<int> EditUser(int user_id, Users user);
        Task<int> DeleteUser(int user_id);
        bool UserExists(int id);
    }
}
