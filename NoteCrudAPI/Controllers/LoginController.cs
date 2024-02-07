using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteCrudAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteCrudAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(IConfiguration configuration, SignInManager<IdentityUser> signInManager , UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {

            var user= Authenticate(login.UserName, login.Password);

           // if (login.UserName == "osmnn" && login.Password == "546739")
           if (user.Result!=null)
            {
                var token = GenerateToken(login.UserName);
                return Ok(token);


            }
            return BadRequest("Giriş Yapılamadı");

        }




        private async Task<IdentityUser?> Authenticate(string userName,string Password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var currentUser=await _userManager.FindByNameAsync(userName);
                if (currentUser != null)
                {
                    return currentUser;
                }
            }
            return null;
        }


        private string GenerateToken(string UserName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, UserName)
            };


            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt:Issuer").Value,
                _configuration.GetSection("Jwt:Audience").Value,
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Token Doğrulama Başarılı");
        }
    }
}
