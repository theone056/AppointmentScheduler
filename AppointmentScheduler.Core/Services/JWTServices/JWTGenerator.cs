using AppointmentScheduler.Core.Domain.IdentityEntities;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.JWTServices.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppointmentScheduler.Core.Services.JWTServices
{
    internal class JWTGenerator : IJWTGenerator
    {
        private readonly IConfiguration _configuration;
        public JWTGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AuthenticationResponse CreateJWTToken(ApplicationUser user)
        {
            try
            {
                DateTime dateExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

                Claim[] claims =
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("DateOfJoing", DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email)
                ];


                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                                                         _configuration["Jwt:Audience"],
                                                                         claims,
                                                                         expires: dateExpiration,
                                                                         signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return new AuthenticationResponse()
                {
                    Email = user.Email,
                    PersonName = user.FullName,
                    Expiration = dateExpiration,
                    Token = token,
                    RefreshToken = GenerateRefreshToken(),
                    RefreshTokenExpirationDateTime = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumber = RandomNumberGenerator.Create();
            randomNumber.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
    