using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using APITest.Domain.Models;
using APITest.Domain.Services;
using APITest.Utils;
using APITest.Resources;
using System;

namespace APITest.Controllers {
    
    public class UserController : Controller {
        private readonly IUserService _userService;

        public UserController(IUserService userService){
            _userService = userService;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetUsersAsync(){
            var users = await _userService.ListAsync();
            var userResources = Mapper.UsersToUserResources(users);
            return userResources;
        }

    }
}