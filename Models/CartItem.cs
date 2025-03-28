namespace GaUMuoi.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }  // ID của sản phẩm
        public string ProductName { get; set; } // Tên sản phẩm
        public string ImageUrl { get; set; } // URL ảnh sản phẩm
        public decimal Price { get; set; } // Giá sản phẩm
        public int Quantity { get; set; } // Số lượng trong giỏ hàng
    }
}
