using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class CategoriesBrand
    {
        [Key]
        public Guid CategoryBrandID { get; set; }

        public Guid CategoryID { get; set; }

        public Guid BrandID { get; set; }

        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
