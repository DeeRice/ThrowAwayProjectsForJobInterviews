using IntegraPartnersContactApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegraPartnersContactApplicationAPI.Interface
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserByID(int ? user_id);
        Task<Users> CreateUser(Users user);
        Task<Users> FindUser(int ? user_id);
        Task<Users> EditUser(int ? user_id, Users user);
        Task<Users> DeleteUser(int ? user_id);
        bool UserExists(int ? id);
        DbSet<Users> PopulateDataSet(DbSet<Users> Users);
    }
}
