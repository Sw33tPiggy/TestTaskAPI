using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APITest.Domain.Models;
using APITest.Domain.Repositories;
using APITest.Presistence.Contexts;

namespace APITest.Presistence.Repositories {
    public class UserRepository : BaseRepositroy, IUserRepository {
        public UserRepository(AppDbContext context) : base(context){

        }

        public async Task<IEnumerable<User>> ListAsync(){
            return await _context.Users.ToListAsync();
        }

        public async Task<User> FindUserByEmailAsync(string email){
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> FindUserByPhoneAsync(string phone)
        {
             return await _context.Users.FirstOrDefaultAsync(user => user.Phone == phone);
        }
    }
}