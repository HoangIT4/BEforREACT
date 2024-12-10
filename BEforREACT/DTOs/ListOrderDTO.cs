namespace BEforREACT.DTOs
{
    public class OrderDetailRes
    {
        public Guid OrderID { get; set; }
        public Guid OrderItemID { get; set; }
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string? Src { get; set; }
        public string? PreImg { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Status { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? CreateAt { get; set; }
    }


    public class CreateOrder
    {
        public Guid UserID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderItemDto
    {
        public Guid CartID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
