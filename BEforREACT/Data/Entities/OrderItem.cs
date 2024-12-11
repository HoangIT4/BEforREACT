using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemID { get; set; }
        [Required]
        public Guid OrderID { get; set; }
        //[Required]
        //public Guid CartID { get; set; }

        public Guid? ProductId { get; set; }

        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Price { get; set; }
        //public virtual Cart? Cart { get; set; }
        public virtual Order Order { get; set; }
    }
}
