using GaUMuoi.Models;
using Microsoft.EntityFrameworkCore;

namespace GaUMuoi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Khai báo các DbSet (Bảng trong Database)
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }

        // Nếu không sử dụng Dependency Injection thì giữ lại
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=GaUMuoi.db");
            }
        }

        // Cấu hình kiểu dữ liệu
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2); // Dùng HasPrecision thay vì HasColumnType

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<GioHang>()
                .Property(g => g.Gia)
                .HasPrecision(18, 2);

            modelBuilder.Entity<GioHang>()
                .Property(g => g.TongTien)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}

// Định nghĩa Model GioHang (KHÔNG chứa DbSet<>)
public class GioHang
{
    public int Id { get; set; }
    public string TenKhachHang { get; set; }
    public string SanPham { get; set; }
    public int SoLuong { get; set; }
    public decimal Gia { get; set; }
    public decimal TongTien { get; set; }
    public DateTime NgayDatHang { get; set; }
}
