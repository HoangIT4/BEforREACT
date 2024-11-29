using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        [Required]
        public Guid? BrandID { get; set; }
        public string? Name { get; set; }

        public string? Src { get; set; }
        public string? PreImg { get; set; }
        public string? Description { get; set; }

        public virtual ProductDetail Detail { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
