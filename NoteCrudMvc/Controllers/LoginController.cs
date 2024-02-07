using Microsoft.AspNetCore.Mvc;

namespace NoteCrudMvc.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
