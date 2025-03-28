using Microsoft.AspNetCore.Mvc;
using GaUMuoi.Models;

namespace GaUMuoi.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Gửi liên hệ thành công!";
                return RedirectToAction("Index");
            }

            return View("Index", contact);
        }
    }
}
