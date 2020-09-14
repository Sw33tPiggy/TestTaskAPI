namespace APITest.Domain.Models.Responses
{
    public class TokenResponse : BaseResponse {
        public AccessToken Token { get; set; }
       
        public TokenResponse(bool success, string message, AccessToken token) : base(success, message) {
            Token = token;
            Success = success;
            Message = message;
        }
    }
}