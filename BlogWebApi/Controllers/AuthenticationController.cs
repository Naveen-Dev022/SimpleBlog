using BlogWebApi.Data;
using BlogWebApi.DTO;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationController(AppDbContext dbContext,IConfiguration configuration)
        {
            _context = dbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserAuthDTO user)
        {
            Tuple< string, string> result = GenerateToken(user);
            var (token,error) = result;
            if (token != string.Empty)
            {
                return Ok(new { Token = token });
            }
            return BadRequest(new { message = error });
        }
        [NonAction]
        private Tuple<string, string> GenerateToken(UserAuthDTO user)
        {
            Authenticate(user,out bool isAuthenticated, out string role, out string email);
            if (isAuthenticated)
            {
                var claims = new[]
{
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),  // Subject (could be username)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID for the token
                new Claim(ClaimTypes.Role, role),  // Name or any additional claim
                new Claim(ClaimTypes.Email,email)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration.GetSection("Jwt:Issuer").Value,
                    audience: _configuration.GetSection("Jwt:audience").Value,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );
                var handler = new JwtSecurityTokenHandler();
                string GenToken = handler.WriteToken(token);
                return new (GenToken, string.Empty);
            }
            return new (string.Empty,"Invalid User!");
        }
        /// <summary>
        /// Authenticating the user is valid or not using Username and Password
        /// if user is not valid returning the state as out param
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isAuthenticated"></param>
        /// <param name="userRole"></param>
        /// <param name="email"></param>
        [NonAction]
        private void Authenticate(UserAuthDTO user, out bool isAuthenticated, out string userRole, out string email)
        {
            var validUser = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (validUser != null)
            {
                userRole = validUser.Role.RoleInfo;
                email = validUser.Email;
                isAuthenticated = true;
            }
            else
            {
                userRole = string.Empty;
                email = string.Empty;
                isAuthenticated = false;
            }
        }

    }
}
