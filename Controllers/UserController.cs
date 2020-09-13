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

        [Route("api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialResource userCredential){
            
            Console.Write("got reqest\n");
            if(userCredential.email == null && userCredential.phone == null ){
                ModelState.AddModelError("login", "use phone or email as login");
            }
            if(!ModelState.IsValid){
                return BadRequest("Error :(");
            }
            User user = null;
            if(userCredential.email != null){
                user = await _userService.FindUserByEmailAsync(userCredential.email);
            }
            if(user == null){
                return Unauthorized("wrong email");
            }

            if(!HashMaker.VerifyHash(userCredential.password, user.PasswordHash)){
                return Unauthorized("wrong password");
            }
            return Ok();
        }
    }
}