using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using ShoesSopAPI.Repository;
using ShoesSopAPI.Repository.Interface;
using ShoesSopAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoesSopAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IConfiguration _config;
        private readonly IAuthenticationRepository _repository;
        public AuthenticationService( IConfiguration config, IAuthenticationRepository repository)
        {
            _config = config;
            _repository = repository;
        }
        public async Task<Account> Login(string username, string password)
        {
            var account = await _repository.FindBySDTAsync(username);
            if (account == null)
            {
                return null;
            }
            else
            {
                if (account.MatKhau == password)
                {
                    Account acc = new Account();
                    var tokenStr = GenerateJWT(account);
                    acc.Id = account.Id;
                    acc.Name = account.HoTen;
                    acc.Username = account.Sđt;
                    acc.Password = account.MatKhau;
                    acc.Token = tokenStr;
                    return acc;
                }
                return null;
            }
        }
        private string GenerateJWT(KhachHang account)
        {
            //  var issuer = _config["Jwt:Issuer"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Auduence"];
            var key = Encoding.ASCII.GetBytes
            (_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddDays(2),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
