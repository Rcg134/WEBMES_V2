using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WEBMES_V2.Controllers
{
    public class LogoutController : Controller
    {
        public async Task<IActionResult> LogoutView()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginView", "Login");
        }
    }
}
