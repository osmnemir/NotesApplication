using Microsoft.AspNetCore.Mvc;

namespace NoteCrudMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly APIGateway aPIGateway;

        public LoginController(APIGateway aPIGateway)
        {
            this.aPIGateway = aPIGateway;
        }


        [HttpGet]
        public IActionResult Index()
        {


            return View();
        }


        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            aPIGateway.Login(username, password);

            return View();
        }
    }
}
