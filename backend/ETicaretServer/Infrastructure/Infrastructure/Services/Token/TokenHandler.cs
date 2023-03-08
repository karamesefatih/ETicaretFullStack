using Application.Abstractions.Token;
using Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Burada o büyük kocaman tokeni oluşturduk
        public Application.Dto_s.Token CreateAccessToken(int minute,User user)
        {
            Application.Dto_s.Token token = new();
            //Security Key simetriğini aldık
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //Şifreli token oluşturduk
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddHours(3).AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                signingCredentials:signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name,user.UserName)}
                ) ;
            //Token oluşturucu sınıfından bir örnek alalım
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccesToken = tokenHandler.WriteToken(securityToken);

            // string refreshToken = CreateRefreshToken();
            token.refreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
