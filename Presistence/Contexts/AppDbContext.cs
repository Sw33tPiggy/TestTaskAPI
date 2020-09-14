using Microsoft.EntityFrameworkCore;
using APITest.Domain.Models;
using System;
using APITest.Utils;

namespace APITest.Presistence.Contexts {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
            

        }

        public DbSet<User> Users {get; set; }
        public DbSet<FileRecord> Files {get; set; }


        protected override void OnModelCreating(ModelBuilder builder){

            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p  => p.Email).IsRequired();
            builder.Entity<User>().Property(p  => p.Phone).IsRequired();
            builder.Entity<User>().Property(p  => p.GivenName).IsRequired();
            builder.Entity<User>().Property(p  => p.Surname).IsRequired();
            builder.Entity<User>().Property(p  => p.PasswordHash).IsRequired();
            builder.Entity<User>().Property(p  => p.MiddleName);
            builder.Entity<User>().HasMany(p => p.Files).WithOne(p => p.Owner).HasForeignKey(p => p.OwnerID);

            builder.Entity<User>().HasData(
                new User{
                    Id = Guid.NewGuid(),
                    GivenName = "Egor",
                    Surname = "Solodaev",
                    MiddleName = "",
                    Email = "test@test.com",
                    Phone = "+13371488",
                    PasswordHash = HashMaker.GetHash("mypassword")
                }
            );

            builder.Entity<FileRecord>().ToTable("Files");
            builder.Entity<FileRecord>().HasKey(p => p.Id);
            builder.Entity<FileRecord>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<FileRecord>().Property(p => p.Name).IsRequired();
            builder.Entity<FileRecord>().Property(p => p.Path).IsRequired();
        }
    }
}