using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductsApp.API.Entities;
using ProductsApp.API.Persistence;

namespace ProductsApp.API.Controllers
{
	[ApiController]
	[Route("api/users")]
    [Authorize]
	public class UsersController : ControllerBase
	{
        private readonly ProductsDb _db;
        private readonly JwtOptions _config;

        public UsersController(ProductsDb db, JwtOptions config)
		{
            _db = db;
            _config = config;
        }

		[HttpPost]
        [AllowAnonymous]
        public IActionResult Post(User user)
        {
            _db.Users.Add(user);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UserUpdateInputModel model)
        {
            var user = _db.Users.SingleOrDefault(u => u.UsuarioId == id);

            if (user is null)
                return NotFound();

            user.Nome = model.Nome;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _db.Users.SingleOrDefault(u => u.UsuarioId == id);

            if (user is null)
                return NotFound();

            _db.Users.Remove(user);

            return NoContent();
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginInputModel model)
        {
            var user = _db.Users.SingleOrDefault(u => u.Email == model.Email && u.Senha == model.Senha);

            if (user is null)
                return NotFound();

            var token = GenerateJSONWebToken(user);

            return Ok(new LoginViewModel { AccessToken = token });
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                new List<Claim>()
                {
                    new Claim("sub", userInfo.Email),
                    new Claim("name", userInfo.Nome),
                    new Claim("aud", _config.Audience)
                },
                expires: DateTime.Now.AddSeconds(_config.ExpirationSeconds),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    

    public class UserUpdateInputModel
    {
        public string Nome { get; set; }
    }

    public class LoginInputModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class LoginViewModel
    {
        public string AccessToken { get; set; }
    }

    public record class JwtOptions(
        string Issuer,
        string Audience,
        string SigningKey,
        int ExpirationSeconds
    );
}

