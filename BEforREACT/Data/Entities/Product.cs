using BEforREACT.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public Guid ProductID { get; set; }

    [Required]
    public Guid? BrandID { get; set; }
    public string? Name { get; set; }
    public string? Src { get; set; } // Ảnh chính
    public string? PreImg { get; set; } // Ảnh preview


    public virtual ProductDetail Detail { get; set; }

    public virtual Brand Brand { get; set; }
    public virtual ICollection<ProductCategory> ProductCategories { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = null;
    public DateTime? DeletedAt { get; set; } = null;
}