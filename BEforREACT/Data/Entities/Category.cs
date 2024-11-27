using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }

        public string CategoryName { get; set; }

        //public ICollection<CategoriesBrand>? CategoriesBrands { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;

    }
}



