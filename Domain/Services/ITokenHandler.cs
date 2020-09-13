using APITest.Domain.Models;

namespace APITest.Domain.Services{
    public interface ITokenHandler{
         AccessToken CreateAccessToken(User user);
    }
}