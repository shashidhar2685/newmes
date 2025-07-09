using System;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newmes.WebAPI.Models;
using Newmes.WebAPI.Repository;
using Newmes.Models;

namespace Newmes.WebAPI.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly AdminRepository repo = new AdminRepository();

        // ✅ Signup
        [HttpPost]
        [Route("signup")]
        public IHttpActionResult Signup(Admin admin)
        {
            var existing = repo.GetAdminByUsername(admin.Username);
            if (existing != null)
                return BadRequest("Username already exists");

            // TODO: Add password hashing here later
            bool success = repo.RegisterAdmin(admin);
            if (success)
                return Ok("Signup successful");
            else
                return BadRequest("Signup failed");
        }

        // ✅ Login
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginModel model)
        {
            var admin = repo.GetAdminByUsername(model.Username);
            if (admin == null || admin.PasswordHash != model.Password)
                return Unauthorized();

            var token = GenerateJwtToken(admin);
            return Ok(new { token });
        }

        // 🔐 Token Generator
        private string GenerateJwtToken(Admin admin)
        {
            var key = Encoding.ASCII.GetBytes("SuperSecretKey_123456789"); // Must be at least 16+ chars
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("AdminId", admin.AdminId.ToString()),
                    new Claim(ClaimTypes.Name, admin.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
