using BudgetBuddy.Domain.Contracts;
using BudgetBuddy.Domain.Models.Dtos;
using BudgetBuddy.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet]
        [Route("GetAllUserDetails")]

        public async Task<ActionResult<UserEntity>> GetAllUserDetails()
        {
            try
            {
                var res = await _userService.GetUserDetailsService();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserDetailsByUserId")]

        public async Task<ActionResult<UserEntity>> GetUserDetailsByUserId(string userId)
        {
            try
            {
                var res = await _userService.GetUserDetailsByUserIdService(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        
        public async Task<ActionResult<string>> CreateUser(CreateUserDto createUserDto) 
        {
            try
            {
                var result = await _userService.CreateUserService(createUserDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("LoginUser")]

        public async Task<ActionResult<string>> LoginUser(LoginDto loginDto)
        {
            try
            {
                var result = await _userService.LoginUserService(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
