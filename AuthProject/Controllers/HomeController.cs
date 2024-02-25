using AuthProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AuthProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
#pragma warning disable IDE0052 // Remover membros particulares não lidos
        private readonly ILogger<HomeController> _logger;
#pragma warning restore IDE0052 // Remover membros particulares não lidos

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Home/LoginRealizado")] // Define a rota para a ação LoginRealizado
        public ActionResult LoginRealizado()
        {
            // Lógica da ação
            return View();
        }

        [HttpPost] // Adiciona o atributo HttpPost
        [Route("Home/AbrirERP")] // Define a rota para a ação AbrirERP
        public IActionResult AbrirERP()
        {
            // Lógica de autenticação, se necessário

            // Redirecionamento para a página de LoginRealizado
            return RedirectToAction("LoginRealizado");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
