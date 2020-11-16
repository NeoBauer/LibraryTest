using Microsoft.AspNetCore.Mvc;

namespace LibraryTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
