using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Expedia.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Expedia.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticateController(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
        )
        {
            this._configuration = configuration;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDto LoginDto
            )
        {
            // 1. validate username & password
            var loginResult = await _signInManager.PasswordSignInAsync(
                LoginDto.Email, LoginDto.Password, false, false
                );
            if (!loginResult.Succeeded)
            {
                return BadRequest();
            }
            var userRepo = await _userManager.FindByNameAsync(LoginDto.Email);
            // 2. create jwt
            // header / payload /signature
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;
            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    userRepo.Id
                ),
            };
            var roleNames = await _userManager.GetRolesAsync(userRepo);
            foreach(var roleName in roleNames)
            {
                var roleClaim = new Claim(ClaimTypes.Role, roleName);
                claims.Add(roleClaim);
            }
            //
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
                [FromBody] RegisterDto registerDto
            )
        {
            // 1. create user
            var user = new IdentityUser()
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            // 2. hash password, and update user
            var result = await _userManager.CreateAsync(
                user,
                registerDto.Password
            );
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            // 3. response
            return Ok();
        }
    } 
}

