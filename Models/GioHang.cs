using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaUMuoi.Models
{
    [Table("GioHangs")] // Đảm bảo tên bảng là "GioHangs" trong database
    public class GioHang
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TenKhachHang { get; set; }

        // Liên kết với bảng Products
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; } // Navigation property

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Gia { get; set; }

        public decimal TongTien => SoLuong * Gia;

        public DateTime NgayDatHang { get; set; } = DateTime.Now;
    }
}
