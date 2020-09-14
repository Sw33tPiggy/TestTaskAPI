using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;
using APITest.Domain.Models.Responses;

namespace APITest.Domain.Services{
    public interface IAuthenticationService{

        Task<TokenResponse> CreateAccessTokenByPhoneAsync(string phone, string password);
        Task<TokenResponse> CreateAccessTokenByEmailAsync(string email, string password);
    }

    
}