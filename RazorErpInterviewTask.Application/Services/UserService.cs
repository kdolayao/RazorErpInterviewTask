using RazorErpInterviewTask.Application.Models;
using RazorErpInterviewTask.Core.Entities;
using RazorErpInterviewTask.Core.Enums;
using RazorErpInterviewTask.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorErpInterviewTask.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository<User, UserAddUpdate, UserLogin> _userRepository;

        public UserService(IUserRepository<User, UserAddUpdate, UserLogin> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id, string role, string company)
        {
            return await _userRepository.GetByIdAsync(id, role, company);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(string role, string company)
        {
            return await _userRepository.GetAllAsync(role, company);
        }

        public async Task AddUserAsync(UserAddUpdate userAddUpdate)
        {
            await _userRepository.AddAsync(userAddUpdate);
        }

        public async Task UpdateUserAsync(int id, UserAddUpdate userAddUpdate)
        {
            await _userRepository.UpdateAsync(id, userAddUpdate);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<string> Auth(UserLogin userLogin)
        {
            return await _userRepository.Auth(userLogin);
        }
    }
}
