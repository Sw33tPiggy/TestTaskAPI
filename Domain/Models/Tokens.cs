using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace APITest.Domain.Models {
    
    public class AccessToken{
        public string Token { get; protected set; }
        public long Expiration { get; protected set; }
        public AccessToken(string token, long expiration)
        {
            if(string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid token.");

            if(expiration <= 0)
                throw new ArgumentException("Invalid expiration.");

            Token = token;
            Expiration = expiration;
        }

        public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
    }

    public class TokenOptions
	{
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public long AccessTokenExpiration { get; set; }
		public long RefreshTokenExpiration { get; set; }
		public string Secret { get; set; }
	}

    public class SigningConfigurations
    {
        public SecurityKey SecurityKey { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(string key)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);

            SecurityKey = new SymmetricSecurityKey(keyBytes);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}