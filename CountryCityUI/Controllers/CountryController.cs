using Microsoft.AspNetCore.Mvc;

namespace CountryCityUI.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
