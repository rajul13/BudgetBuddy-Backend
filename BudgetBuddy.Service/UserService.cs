using AutoMapper;
using BudgetBuddy.Domain.Contracts;
using BudgetBuddy.Domain.Models.Dtos;
using BudgetBuddy.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace BudgetBuddy.Service
{
    public class UserService : IUserService
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)

        {
            _mapper = mapper;
            _userRepository = userRepository;

        }
        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            byte[] storedHashBytes = Convert.FromBase64String(storedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(storedHashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                for (int i = 0; i < 32; i++)
                {
                    if (storedHashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }



        public async Task<List<UserDto>> GetUserDetailsService()
        {
            try
            { 
                var user = await _userRepository.GetUserDetails();
                if (user == null)
                { 
                    return new List<UserDto>();
                }
                else 
                {
                    var mappedToken = _mapper.Map<List<UserDto>>(user);
                    return mappedToken;
                }              
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDto>GetUserDetailsByUserIdService(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserDetailsByUserId(userId);
                var mappedToken = _mapper.Map<UserDto>(user);
                return mappedToken;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string>CreateUserService(CreateUserDto createUserDto)
        {
            try
            {
                if (!IsValidEmail(createUserDto.Email))
                {
                    return "Invalid email format.";
                }

                if (!IsValidPhoneNumber(createUserDto.PhoneNumber))
                {
                    return "Invalid phone number format.";
                }
              
                var existingUsers = await _userRepository.GetUserDetails();
             
                if (existingUsers != null && existingUsers.Any())
                {
                    if (existingUsers.Any(user => user.UserName == createUserDto.UserName))
                    {
                        return "Username already exists.";
                    }

                    if (existingUsers.Any(user => user.Email == createUserDto.Email))
                    {
                        return "Email already exists.";
                    }

                    if (existingUsers.Any(user => user.PhoneNumber == createUserDto.PhoneNumber))
                    {
                        return "Phone number already exists.";
                    }
                }


                string hashedPassword = HashPassword(createUserDto.PasswordHash);

                var newUser = new UserEntity
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = createUserDto.UserName,
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    Email = createUserDto.Email,
                    PhoneNumber = createUserDto.PhoneNumber,
                    PasswordHash = hashedPassword,
                    CreatedDate = DateTime.UtcNow,
                };

                var result = await _userRepository.CreateUser(newUser);
                return result;
            }
            catch
            {
                throw;
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;    
            var phoneRegex = @"^[6-9]\d{9}$";
            return Regex.IsMatch(phoneNumber, phoneRegex);
        }

        public async Task<string> LoginUserService(LoginDto loginDto)
        {
            try
            {
              
                var user = await _userRepository.GetUserDetailsByEmailOrUsername(loginDto.EmailOrUserName);
                if (user == null)
                {
                    return "User not found.";
                }
                if (!VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                    return "Invalid password.";
                }

               
                return "Login successful.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during login.", ex);
            }
        }

    }
}
