using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthProject.Models;


namespace AuthProject.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {

            if (modelLogin.Email == "user@example.com" &&
                modelLogin.PassWord == "123"
                )
            {
#pragma warning disable IDE0090 // Usar 'new(...)'
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties","Example Role")
                
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme );
#pragma warning restore IDE0090 // Usar 'new(...)'

#pragma warning disable IDE0090 // Usar 'new(...)'
                AuthenticationProperties properties = new AuthenticationProperties() { 
                
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };
#pragma warning restore IDE0090 // Usar 'new(...)'

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
            
                return RedirectToAction("Index", "Home");
            }



            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
    }
}
