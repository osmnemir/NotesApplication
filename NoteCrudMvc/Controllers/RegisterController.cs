using Microsoft.AspNetCore.Mvc;

namespace NoteCrudMvc.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
