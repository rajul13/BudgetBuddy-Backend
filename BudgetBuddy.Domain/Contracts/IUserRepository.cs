using BudgetBuddy.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuddy.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetUserDetails();
        Task<UserEntity> GetUserDetailsByUserId(string userId);
        Task<string> CreateUser(UserEntity userDetails);
        Task<UserEntity> GetUserDetailsByEmailOrUsername(string emailOrUserName);
    }
}
