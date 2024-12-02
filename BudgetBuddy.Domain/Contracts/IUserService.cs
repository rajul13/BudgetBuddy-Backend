using BudgetBuddy.Domain.Models.Dtos;
using BudgetBuddy.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuddy.Domain.Contracts
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUserDetailsService();
        Task<UserDto> GetUserDetailsByUserIdService(string userId);
        Task<string> CreateUserService(CreateUserDto createUserDto);
        Task<string> LoginUserService(LoginDto loginDto);
    }
}
