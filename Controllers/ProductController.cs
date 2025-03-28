using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using GaUMuoi.Data;
using GaUMuoi.Models;

public class ProductController : Controller
{
    private readonly GaUMuoiDbContext _context;

    public ProductController(GaUMuoiDbContext context)
    {
        _context = context;
    }

    // 📌 Danh sách sản phẩm
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync();

        // Kiểm tra quyền Admin để hiển thị nút sửa, xóa trên giao diện
        ViewBag.IsAdmin = HttpContext.Session.GetString("Role") == "Admin";

        return View(products);
    }

    // ➕ Thêm sản phẩm
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); // 🔥 Lưu vào database
            return RedirectToAction("Index");
        }
        return View(product);
    }

    // ✏️ Sửa sản phẩm
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Update(product);
            _context.SaveChanges(); // 🔥 Lưu vào database
            return RedirectToAction("Index");
        }
        return View(product);
    }

    // 🗑️ Xóa sản phẩm
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
