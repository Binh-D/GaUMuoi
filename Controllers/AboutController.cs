using Microsoft.AspNetCore.Mvc;

namespace GaUMuoi.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
