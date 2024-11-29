using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public Guid CategoryID { get; set; }

    public string? CategoryName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } // Thêm virtual cho phép lazy loading
}