using System.Threading.Tasks;
using APITest.Utils;
using APITest.Domain.Services;
using APITest.Services;

namespace APITest.Services{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler){
            _tokenHandler = tokenHandler;
            _userService = userService;
        }
        public async Task<TokenResponse> CreateAccessTokenByEmailAsync(string email, string password)
        {
            var user = await _userService.FindUserByEmailAsync(email);
            if(user == null || !HashMaker.VerifyHash(password, user.PasswordHash)){
                return new TokenResponse(false, "Invalid credentials.", null);
            }

            var token = _tokenHandler.CreateAccessToken(user);

            return new TokenResponse(true, null, token);
        }

        public async Task<TokenResponse> CreateAccessTokenByPhoneAsync(string phone, string password)
        {
            var user = await _userService.FindUserByPhoneAsync(phone);
            if(user == null || !HashMaker.VerifyHash(password, user.PasswordHash)){
                return new TokenResponse(false, "Invalid credentials.", null);
            }

            var token = _tokenHandler.CreateAccessToken(user);

            return new TokenResponse(true, null, token);
        }
    }
}