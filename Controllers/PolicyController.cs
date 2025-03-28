using Microsoft.AspNetCore.Mvc;

namespace GaUMuoi.Controllers
{
    public class PolicyController : Controller
    {
        // Hiển thị trang chính sách
        public ActionResult Index()
        {
            return View();
        }

        // Hiển thị nội dung "Hướng dẫn mua hàng"
        public ActionResult HuongDanMuaHang()
        {
            return PartialView();
        }
    }
}
