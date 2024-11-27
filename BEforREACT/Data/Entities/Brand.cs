using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class Brand
    {
        [Key]
        public Guid BrandID { get; set; }

        public string BrandName { get; set; }

        //public ICollection<CategoriesBrand>? CategoriesBrands { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
