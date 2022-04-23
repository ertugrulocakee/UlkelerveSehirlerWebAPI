using Microsoft.AspNetCore.Mvc;

namespace CountryCityUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
