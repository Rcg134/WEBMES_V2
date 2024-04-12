using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEBMES_V2.Controllers
{
    [Authorize(Policy = "UserCred")]
    public class MainPageController : Controller
    {
        public IActionResult MainPageView()
        {
            return View();
        }
    }
}
