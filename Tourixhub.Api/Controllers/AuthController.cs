using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tourixhub.Application.Helpers;
using Tourixhub.Domain.Entities;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Application.Controllers
{
    [AllowAnonymous]
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly string _jwtSecret;
        public AuthController(
            IAuthService authService, 
            IOptions<AppSettings> appSettings
            )
        {
            _jwtSecret = appSettings.Value.JWTsecret;
            _authService = authService;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserRegistrationDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }

            var result = await _authService.Signup(userDto);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.IsAppUserExit(loginDto);
                if(user != null)
                {
                    var signinKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_jwtSecret));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserId", user.Id.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(30),
                        SigningCredentials = new SigningCredentials(
                            signinKey,
                            SecurityAlgorithms.HmacSha256Signature
                            )

                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);

                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                } 

            }

            return BadRequest(new { message = "Invalid Information" });

        }
    }
}
