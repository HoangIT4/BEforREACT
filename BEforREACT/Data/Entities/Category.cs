using BEforREACT.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public Guid CategoryID { get; set; }

    public string? CategoryName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    public virtual ICollection<CategoriesBrand> CategoriesBrands { get; set; }
}