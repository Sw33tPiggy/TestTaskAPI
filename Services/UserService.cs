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

        public async Task<User> UserByEmailAsync(string email){
            return await _userRepository.UserByEmailAsync(email);
        }
    }
}