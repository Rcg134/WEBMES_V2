using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WEBMES_V2.Models;
using WEBMES_V2.Models.DomainModels;
using WEBMES_V2.Models.Models.ISQLRepository;
using static WEBMES_V2.Models.Models.ISQLRepository.ILoginRepository;
using WEBMES_V2.Models.DTO;

namespace WEBMES_V2.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepoConnection _loginRepository;

        public LoginController(ILogger<LoginController> logger ,
                               ILoginRepoConnection loginRepository)
        {
            _logger = logger;
            this._loginRepository = loginRepository;
        }

        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginView(UserDTO usrDTO)
        {

            if (ModelState.IsValid)
            {

                var isExisst =await _loginRepository.UserLogin(usrDTO);

                if (isExisst != false)
                {
                    List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier,usrDTO.UserCode.ToString())
                        };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                   new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("MainPageView", "MainPage");

                }
            }
            ModelState.AddModelError(string.Empty, "Username and Password is invalid");
            return View();
       
        }

    }
}
