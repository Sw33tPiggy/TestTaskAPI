using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APITest.Domain.Models;
using APITest.Domain.Repositories;
using APITest.Presistence.Contexts;
using System;
using System.Linq;

namespace APITest.Presistence.Repositories {
    public class FileRepository : BaseRepositroy, IFileRepository {


        public FileRepository(AppDbContext context) : base(context){

        }

        public async Task<IEnumerable<FileRecord>> ListAsync(Guid userId){
            return await _context.Files.Where(p => p.OwnerID == userId).ToListAsync();
        }

        public async Task<User> FindUserByEmailAsync(string email){
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> FindUserByPhoneAsync(string phone)
        {
             return await _context.Users.FirstOrDefaultAsync(user => user.Phone == phone);
        }

        public async Task<FileRecord> FindFileByIdAsync(Guid Id, Guid UserId)
        {
            return await _context.Files.FirstOrDefaultAsync(file => file.Id == Id && file.Owner.Id == UserId);
        }

        public void Add(FileRecord fileRecord)
        {
            _context.Files.Add(fileRecord);
            _context.SaveChanges();
        }
    }
}