using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;

namespace APITest.Domain.Services{
    public interface IAuthenticationService{

        Task<TokenResponse> CreateAccessTokenByPhoneAsync(string phone, string password);
        Task<TokenResponse> CreateAccessTokenByEmailAsync(string email, string password);
         Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail);
         void RevokeRefreshToken(string refreshToken);
    }

    public class TokenResponse{
        public AccessToken Token { get; set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public TokenResponse(bool success, string message, AccessToken token){
            Token = token;
            Success = success;
            Message = message;
        }
    }
}