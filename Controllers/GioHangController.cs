using GaUMuoi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaUMuoi.Data;


namespace GaUMuoi.Controllers
{
    public class GioHangController : Controller
    {
        private readonly GaUMuoiDbContext _context;

        public GioHangController(GaUMuoiDbContext context)
        {
            _context = context;
        }

        // Hiển thị giỏ hàng
        public IActionResult Index()
        {
            var gioHang = _context.GioHangs.ToList();
            return View(gioHang);
        }

        // Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public IActionResult ThemVaoGioHang(string tenKhachHang, string sanPham, int soLuong, decimal gia)
        {
            if (string.IsNullOrEmpty(tenKhachHang) || string.IsNullOrEmpty(sanPham) || soLuong <= 0 || gia <= 0)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ!";
                return RedirectToAction("Index");
            }

            var gioHang = new GioHang
            {
                TenKhachHang = tenKhachHang,
                SanPham = sanPham,
                SoLuong = soLuong,
                Gia = gia,
                TongTien = soLuong * gia,
                NgayDatHang = DateTime.Now
            };

            _context.GioHangs.Add(gioHang);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // API lấy số lượng sản phẩm trong giỏ hàng
        [HttpGet("GioHang/GetCartCount")]
        public IActionResult GetCartCount()
        {
            var count = _context.GioHangs.Count();
            return Json(count);
        }

        // Thanh toán
        [HttpPost]
        public IActionResult ThanhToan(string tenKhachHang)
        {
            if (string.IsNullOrEmpty(tenKhachHang))
            {
                TempData["Error"] = "Tên khách hàng không được để trống!";
                return RedirectToAction("Index");
            }

            var gioHang = _context.GioHangs.Where(g => g.TenKhachHang == tenKhachHang).ToList();
            if (!gioHang.Any())
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction("Index");
            }

            _context.GioHangs.RemoveRange(gioHang);
            _context.SaveChanges();

            TempData["Success"] = "Thanh toán thành công!";
            return RedirectToAction("Index");
        }
    }
}
