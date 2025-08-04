using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleLoginApi.Dtos;
using SimpleLoginApi.Models;
using SimpleLoginApi.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleLoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(UserRegisterRequestDto request)
        {
            var user = new User()
            {
                Id = FakeUserStorage.Id++,
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Role = "User"
            };
            FakeUserStorage.Users.Add(user);
            return Created();

        }
        [HttpPost("login")]
        public IActionResult Login(UserLoginRequestDto request)
        {
            var user = FakeUserStorage.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if(user is null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jbQ9jkOJ0kLfixQeXkdFw5XcKl0qzuFu"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials:creds
                    );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(new {token});
        }
    }

}
