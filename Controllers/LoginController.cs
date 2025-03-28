using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using GaUMuoi.Data;
using GaUMuoi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;

public class LoginController : Controller
{
    private readonly GaUMuoiDbContext _context;

    public LoginController(GaUMuoiDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string TenDangNhap, string MatKhau)
    {
        var user = _context.TaiKhoans.FirstOrDefault(u => u.TenDangNhap == TenDangNhap && u.MatKhau == MatKhau);

        if (user != null)
        {
            // 🔹 Lưu vai trò vào Session
            HttpContext.Session.SetString("Role", user.VaiTro);

            // 🔹 Lưu thông tin vào Identity (nếu dùng)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.Role, user.VaiTro)  // Admin/User
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Product"); // Đăng nhập thành công
        }

        ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
        return View("Index"); // Hiển thị lại trang đăng nhập với thông báo lỗi
    }

    public async Task<IActionResult> Logout()
    {
        // Xóa session và đăng xuất Identity
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }
}
