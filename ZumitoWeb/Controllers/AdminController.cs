using Microsoft.AspNetCore.Mvc;

namespace ZumitoWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Logged") == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Salir()
        {
            HttpContext.Session.SetString("Logged", "false");
            return RedirectToAction("Index", "Home");
        }
    }
}
