namespace BEforREACT.DTOs
{
    public class OrderCreateDto
    {
        public Guid UserId { get; set; } // ID của người dùng
        public string Fullname { get; set; } // ID của người dùng

        public string? Address { get; set; } // Địa chỉ
        public string? City { get; set; } // Thành phố
        public string? Ward { get; set; } // Phường/Xã
        public string? District { get; set; } // Quận/Huyện
        public string? PhoneNumber { get; set; } // Số điện thoại
        public string? PaymentMethod { get; set; } // Phương thức thanh toán
    }

}