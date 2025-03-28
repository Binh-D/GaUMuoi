using GaUMuoi.Data;
using GaUMuoi.Models;
using System.Collections.Generic;
using System.Linq;

public static class DbInitializer
{
    public static void Seed(GaUMuoiDbContext context)
    {
        // Kiểm tra nếu bảng Products chưa có dữ liệu thì thêm
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Gà Ủ Muối Nguyên Con", Description = "Gà ủ muối nguyên con, thơm ngon", Price = 290000, ImageUrl = "/images/gaumuoinguyencon.jpg" },
                new Product { Name = "Gà Ủ Muối Nửa Con", Description = "Nửa con gà ủ muối, phù hợp cho 1-2 người", Price = 160000, ImageUrl = "/images/gaumuoinuacon.jpg" },
                new Product { Name = "Chân Gà Rút Xương Ủ Muối", Description = "Chân gà dai giòn, sần sật", Price = 140000, ImageUrl = "/images/changarutxuongumuoi.jpg" }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        // Thêm tài khoản admin nếu chưa có
        if (!context.TaiKhoans.Any())
        {
            var admin = new TaiKhoan
            {
                TenDangNhap = "admin",
                MatKhau = "admin123",
                VaiTro = "Admin"
            };

            context.TaiKhoans.Add(admin);
            context.SaveChanges();
        }
    }
}

