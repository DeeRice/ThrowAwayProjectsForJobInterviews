using IntegraPartnersContactApplicationAPI.Interface;
using IntegraPartnersContactApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;

namespace IntegraPartnersContactApplicationAPI.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IntegraPartnersContactAPIDataContext appDbContext;

        public UsersRepository(IntegraPartnersContactAPIDataContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await appDbContext.Users.ToListAsync();
        }

        public async Task<Users> GetUserByID(int user_id)
        {
            return await appDbContext.Users
                .FirstOrDefaultAsync(e => e.user_id == user_id);
        }

        public async Task<int> CreateUser(Users user)
        {
            if (user != null && user.user_id != 0)
            {
                var result = await appDbContext.Users.AddAsync(user);
                await appDbContext.SaveChangesAsync();
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> EditUser(int user_id, Users user)
        {
            var result = await appDbContext.Users
                .FirstOrDefaultAsync(e => e.user_id == user.user_id);

            if (result != null)
            {
                result.first_name = user.first_name;
                result.last_name = user.last_name;
                result.email = user.email;
                result.department = user.department;
                result.user_status = user.user_status;
                await appDbContext.SaveChangesAsync();

                return 1;
            }

            return -1;
        }

        public async Task<int> DeleteUser(int user_id)
        {
            var result = await appDbContext.Users
                .FirstOrDefaultAsync(e => e.user_id == user_id);
            if (result != null)
            {
                appDbContext.Users.Remove(result);
                await appDbContext.SaveChangesAsync();
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public async Task<Users> FindUser(int user_id)
        {
            var user = await appDbContext.Users.FindAsync(user_id);
            return user;
        }

        public bool UserExists(int id)
        {
            var result = appDbContext.Users.Any(e => e.user_id == id);
            return result;
        }
    }
}