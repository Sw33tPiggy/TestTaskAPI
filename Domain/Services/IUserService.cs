using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;

namespace APITest.Domain.Services{
    public interface IUserService{
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindUserByEmailAsync(string email);
        Task<User> FindUserByPhoneAsync(string phone);
    }
}