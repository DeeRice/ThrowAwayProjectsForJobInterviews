using IntegraPartnersContactApplicationAPI.Interface;
using IntegraPartnersContactApplicationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;


namespace IntegraPartnersContactApplicationAPI.Repository
{
    public class UsersRepository : IUsersRepository
    {
        public IntegraPartnersContactAPIDataContext _appDbContext ;


        public UsersRepository(IntegraPartnersContactAPIDataContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<Users> GetUserByID(int ? user_id)
        {
            try
            {
                return await _appDbContext.Users
                    .FirstOrDefaultAsync(e => e.user_id == user_id);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<Users> CreateUser(Users user)
        {
            if (user != null)
            {
                try
                {
                    user.user_id = null;
                    var result = await _appDbContext.Users.AddAsync(user);

                    await _appDbContext.SaveChangesAsync();
                    Users updatedUser = _appDbContext.Users.FirstOrDefault(x => x.user_id == user.user_id);
                    return updatedUser;
                }
                catch(Exception ex) 
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<Users> EditUser(int ? user_id, Users user)
        {
            try
            {
                var result = await _appDbContext.Users
                    .FirstOrDefaultAsync(e => e.user_id == user.user_id);

                if (result != null)
                {
                    result.user_name = user.user_name;
                    result.user_id = user.user_id;
                    result.first_name = user.first_name;
                    result.last_name = user.last_name;
                    result.email = user.email;
                    result.department = user.department;
                    result.user_status = user.user_status;
                    await _appDbContext.SaveChangesAsync();

                    Users updatedUser = _appDbContext.Users.FirstOrDefault(x => x.user_id == user.user_id);
                    return updatedUser;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<Users> DeleteUser(int ? user_id)
        {
            var result = await _appDbContext.Users
                .FirstOrDefaultAsync(e => e.user_id == user_id);
            if (result != null)
            {
                var removeUser = _appDbContext.Users.FirstOrDefault(x => x.user_id == user_id);
                _appDbContext.Users.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return removeUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<Users> FindUser(int ? user_id)
        {
            var user = await _appDbContext.Users.FindAsync(user_id);
            return user;
        }

        public bool UserExists(int ? id)
        {
            var result = _appDbContext.Users.Any(e => e.user_id == id);
            return result;
        }

        public DbSet<Users> PopulateDataSet(DbSet<Users> Users)
        {
            _appDbContext.Users = Users;
            return _appDbContext.Users;
        }
    }
}