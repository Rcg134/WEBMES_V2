using Microsoft.AspNetCore.Mvc;

namespace WEBMES_V2.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult MainPageView()
        {
            return View();
        }
    }
}
