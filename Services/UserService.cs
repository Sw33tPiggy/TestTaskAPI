using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;
using APITest.Domain.Services;
using APITest.Domain.Repositories;

namespace APITest.Services{
    public class UserService : IUserService {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository){
            _userRepository = userRepository;
        } 
        public async Task<IEnumerable<User>> ListAsync(){
            return await _userRepository.ListAsync();
        }

        public async Task<User> FindUserByEmailAsync(string email){
            return await _userRepository.FindUserByEmailAsync(email);
        }

        public async Task<User> FindUserByPhoneAsync(string phone)
        {
            return await _userRepository.FindUserByPhoneAsync(phone);
        }
    }
}