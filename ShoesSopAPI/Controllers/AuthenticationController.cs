﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoesSopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DBShop _context;

        public AuthenticationController(DBShop context)
        {
            _context = context;
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<Account>> Login(string username, string password)
        {
            var account = await _context.KhachHangs.Where(n => n.Sđt == username).FirstOrDefaultAsync();
            IActionResult response = Unauthorized();

            if (account == null)
            {
                return NotFound();
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
                return NotFound();
            }
        }
        private string GenerateJWT(KhachHang account)
        {
            //  var issuer = _config["Jwt:Issuer"];
            var issuer = "https://fdgfdgfdsgdfgdfg.com/";
            var audience = "https://hgfnfgbseffdvf.com/";
            var key = Encoding.ASCII.GetBytes
            ("This is a sample secret key - please don't use in production environment.'");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
               /* new Claim(JwtRegisteredClaimNames.Name, account.HoTen),
                new Claim(JwtRegisteredClaimNames.Email, account.Email),*/
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler() ;
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}