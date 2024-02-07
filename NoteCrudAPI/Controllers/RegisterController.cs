using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteCrudAPI.Models;

namespace NoteCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(string userName, string Password)
        {
            IdentityUser user=new IdentityUser(userName);
            var result= await _userManager.CreateAsync(user,Password);
            
            if (result.Succeeded)
            {
                return Ok("Kullanıcı Kaydı Başarılı");


            }
            return BadRequest("Kullanıcı Kaydı Yapılamadı");

        }



    }
}
