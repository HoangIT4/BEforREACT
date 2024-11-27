using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class Cart
    {
        [Key]
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
