using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Auth.Demo
{
    public interface IJWTAuthenticationManager
    {
        AuthenticationResponse Authenticate(string username, string password);
        IDictionary<string, string> UsersRefreshTokens { get; set; }
        AuthenticationResponse Authenticate(string username, Claim[] claims);
    }

    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" }
        };

        public IDictionary<string, string> UsersRefreshTokens { get; set; }

        private readonly string tokenKey;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;

        public JWTAuthenticationManager(string tokenKey, IRefreshTokenGenerator refreshTokenGenerator)
        {
            this.tokenKey = tokenKey;
            this.refreshTokenGenerator = refreshTokenGenerator;
            UsersRefreshTokens = new Dictionary<string, string>();
        }

        public AuthenticationResponse Authenticate(string username, Claim[] claims)
        {
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var jwtSecurityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            var token =new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = refreshTokenGenerator.GenerateToken();

            if (UsersRefreshTokens.ContainsKey(username))
            {
                UsersRefreshTokens[username] = refreshToken;
            }
            else
            {
                UsersRefreshTokens.Add(username, refreshToken);
            }

            return new AuthenticationResponse
            {
                JwtToken = token,
                RefreshToken = refreshToken
            };
        }

        public AuthenticationResponse Authenticate(string username, string password)
        {
            if (!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = refreshTokenGenerator.GenerateToken();

            if (UsersRefreshTokens.ContainsKey(username))
            {
                UsersRefreshTokens[username] = refreshToken;
            }
            else
            {
                UsersRefreshTokens.Add(username, refreshToken);
            }

            return new AuthenticationResponse
            {
                JwtToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken
            };
        }
    }
}