using BudgetBuddy.DAL.DBContext;
using BudgetBuddy.Domain.Contracts;
using BudgetBuddy.Domain.Models.Dtos;
using BudgetBuddy.Domain.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuddy.DAL.Repositories
{
    public class UserRepository :  IUserRepository
    {
        public readonly BudgetBuddyDbContext _context;



        public UserRepository(BudgetBuddyDbContext serviceContext)
        {
            _context = serviceContext;

        }

        public async Task<List<UserEntity>> GetUserDetails()
        {
            try
            {
                return await _context.User.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserEntity> GetUserDetailsByUserId(string userId)
        {
            try
            {
                var result = await _context.User.FirstOrDefaultAsync(x => x.UserId == userId);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string>CreateUser(UserEntity userDetails)
        {
            try
            {
                _context.User.AddAsync(userDetails);
                await _context.SaveChangesAsync();
                return "User created successfully.";
            }
            catch
            {
                throw;
            }
        }
        public async Task<UserEntity> GetUserDetailsByEmailOrUsername(string emailOrUserName)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == emailOrUserName 
                            || u.UserName == emailOrUserName);
        }

    }
}
