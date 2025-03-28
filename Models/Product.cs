using System.ComponentModel.DataAnnotations;

namespace GaUMuoi.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Giá không được để trống!")]
        [Range(1000, 10000000, ErrorMessage = "Giá phải lớn hơn 1000 VNĐ!")]
        public decimal Price { get; set; }
        public string Description { get; set; }  // Thêm thuộc tính mô tả
        public string ImageUrl { get; set; }    // Thêm thuộc tính hình ảnh
    }
}
