using Microsoft.EntityFrameworkCore;
using GaUMuoi.Models;

namespace GaUMuoi.Data
{
    public class GaUMuoiDbContext : DbContext
    {
        public GaUMuoiDbContext(DbContextOptions<GaUMuoiDbContext> options)
            : base(options) { }

        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<Order> Orders { get; set; }
      


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa bảng cho Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)"); // Định dạng kiểu tiền tệ
            });

            // Định nghĩa bảng cho TaiKhoan
            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.ToTable("TaiKhoans");
                entity.HasKey(t => t.Id); // Định nghĩa khóa chính

                entity.Property(t => t.TenDangNhap)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(t => t.MatKhau)
                      .IsRequired()
                      .HasMaxLength(255);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
