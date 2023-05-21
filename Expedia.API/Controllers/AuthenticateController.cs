using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Expedia.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Expedia.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticateController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        public IActionResult Login(
            [FromBody] LoginDto LoginDto
            )
        {
            // 1. validate username & password
            // 2. create jwt
            // header / payload /signature
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;
            var claims = new []
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    "fake_user_id"
                ),
                new Claim(
                    ClaimTypes.Role,
                    "Admin"
                    )
            };
            var secretByte = Encoding.UTF8.GetBytes(
                _configuration["Authenitication:SecretKey"]
                );
            var signingKey = new SymmetricSecurityKey(secretByte);
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm);
            var token = new JwtSecurityToken(
                issuer: _configuration["Authenitication:Issuer"],
                audience: _configuration["Authenitication:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(Int32.Parse(
                    _configuration["Authenitication:ExpiresDays"])),
                signingCredentials
            );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            // 3. return 200 ok + jwt
            return Ok(tokenStr);
        }
    }
}

