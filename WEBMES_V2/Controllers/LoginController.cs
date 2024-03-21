using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBMES_V2.Models;

namespace WEBMES_V2.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit()
        {

            return RedirectToAction("MainPageView", "MainPage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
