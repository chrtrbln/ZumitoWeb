using Microsoft.AspNetCore.Mvc;
using ZumitoWeb.Data;
using ZumitoWeb.ViewModels;

namespace ZumitoWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ZumitoWebContext _context;

        public LoginController(ZumitoWebContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ingresar(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Usuario o PIN incorrectos.";
                return View("Index", model);
            }
                

            var empleado = _context.Empleado.FirstOrDefault(u => u.Name == model.Nombre && u.Pass == model.Pass);

            if (empleado != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var usuario = _context.Cliente.FirstOrDefault(c => c.Nombre == model.Nombre && c.Pass == model.Pass);
                if (usuario != null)
                {
                    ViewBag["User"] = usuario.Nombre;
                    return View("Index", "Home");
                }
            }
            ViewBag.Error = "Usuario o PIN incorrectos.";
            return View("Index", model);
        }
    }
}
