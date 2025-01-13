using Microsoft.AspNetCore.Mvc;

namespace TestMVC.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
