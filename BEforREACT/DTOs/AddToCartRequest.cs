using System.ComponentModel.DataAnnotations.Schema;

namespace BEforREACT.DTOs
{
    public class AddToCartRequest
    {

        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public Guid UserID { get; set; }
        public bool isMultiple { get; set; } = false;
    }
    public class AddToCartRes
    {
        public Guid CartID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public Guid UserID { get; set; }
        public string? Name { get; set; }
        public bool isMultiple { get; set; }
        public string? Src { get; set; }
        public string? PreImg { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }
        public decimal? Total { get; set; }
        public List<string>? Categories { get; set; }
        public string FormattedPrice => Price.ToString("#0.000").Replace(",", ".") ?? "0.000";
        public string TotalPrice => Total?.ToString("#0.000").Replace(",", ".") ?? "0.000";
    }

    public class ClearCart
    {
        public Guid UserID { get; set; }
    }
    public class DeleteCartItem
    {
        public Guid UserID { get; set; }
        public Guid CartId { get; set; }

    }
}