using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class ProductCategory
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductID { get; set; }
        public Guid CategoryID { get; set; }
    }
}
