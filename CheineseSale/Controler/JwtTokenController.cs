using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtTokenController : ControllerBase
    {
        
            private readonly GiftsDbContext _context;

            private readonly JwtTokenService _jwtTokenService;
        private readonly PasswordHasher<User> _passwordHasher;

        public JwtTokenController(JwtTokenService jwtTokenService, GiftsDbContext context)
            {
                _jwtTokenService = jwtTokenService;
                _context = context;
             _passwordHasher = new PasswordHasher<User>();

            }

        [HttpPost("login")]

        public IActionResult Login([FromBody] UserLogin request)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == request.UserName);

            if (user == null)
            {
                return Unauthorized("משתמש לא נמצא");
            }

            var pass = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!pass)
            {
                return Unauthorized("הסיסמה לא תואמת");
            }

            var roles = new List<string> { user.UserRole };
            var token = _jwtTokenService.GenerateJwtToken(request.UserName, roles);

            return Ok(new { Token = token });
        }

    }

}
    

