using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;

namespace APITest.Domain.Repositories{
    public interface  IUserRepository{
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindUserByEmailAsync(string email);
        Task<User> FindUserByPhoneAsync(string email);
    }
}