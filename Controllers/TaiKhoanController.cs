using System.Linq;
using GaUMuoi.Data;
using GaUMuoi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class TaiKhoanController : Controller
{
    private readonly GaUMuoiDbContext _context;

    public TaiKhoanController(GaUMuoiDbContext context)
    {
        _context = context;
    }

    public IActionResult DangNhap()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DangNhap(string tenDangNhap, string matKhau)
    {
        var user = _context.TaiKhoans.FirstOrDefault(u => u.TenDangNhap == tenDangNhap && u.MatKhau == matKhau);

        if (user != null)
        {
            HttpContext.Session.SetString("User", user.TenDangNhap);
            HttpContext.Session.SetString("Role", user.VaiTro); // Lưu vai trò vào session

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
        return View();
    }


    public IActionResult DangXuat()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
