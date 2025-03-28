using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GaUMuoi.Data;
using GaUMuoi.Models;
using System.Linq;

public class SanPhamController : Controller
{
    private readonly GaUMuoiDbContext _context;

    public SanPhamController(GaUMuoiDbContext context)
    {
        _context = context;
    }

    // Xóa sản phẩm (Chỉ Admin)
    [HttpPost]
    public IActionResult Xoa(int id)
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return Unauthorized(); // Trả về lỗi nếu không phải Admin
        }

        var sanPham = _context.Products.Find(id);
        if (sanPham != null)
        {
            _context.Products.Remove(sanPham);
            _context.SaveChanges();
        }

        return Ok();
    }

    // Cập nhật sản phẩm (Chỉ Admin)
    [HttpPost]
    public IActionResult CapNhat(int id, string field, string value)
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return Unauthorized();
        }

        var sanPham = _context.Products.Find(id);
        if (sanPham == null) return NotFound();

        if (field == "Ten")
        {
            sanPham.Name = value;
        }
        else if (field == "Gia")
        {
            if (decimal.TryParse(value, out decimal gia))
            {
                sanPham.Price = gia;
            }
            else
            {
                return BadRequest("Giá không hợp lệ.");
            }
        }

        _context.SaveChanges();
        return Ok();
    }
}
