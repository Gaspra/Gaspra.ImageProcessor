using Microsoft.AspNetCore.Mvc;

namespace ImageProcessor.Website.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}