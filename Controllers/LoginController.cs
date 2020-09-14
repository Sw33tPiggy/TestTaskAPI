using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APITest.Domain.Services;
using APITest.Resources;
using APITest.Utils;
using APITest.Domain.Models.Responses;

namespace APITest.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialResource userCredential){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            TokenResponse response;
            if (userCredential.email != null){
                response = await _authenticationService.CreateAccessTokenByEmailAsync(userCredential.email, userCredential.password);
            } else {
                if (userCredential.phone != null){
                    response = await _authenticationService.CreateAccessTokenByPhoneAsync(userCredential.phone, userCredential.password);
                } else {
                    return BadRequest();
                }
            }

            if(!response.Success){
                return BadRequest(response.Message);
            }

            var accessTokenResource = Mapper.AccessTokenResourceToAccessToken(response.Token);
            return Ok(accessTokenResource);
        }
    }
}