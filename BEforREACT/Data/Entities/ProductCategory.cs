using System.ComponentModel.DataAnnotations;

public class ProductCategory
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductID { get; set; }
    public Guid CategoryID { get; set; }

    public virtual Product Product { get; set; }
    public virtual Category Category { get; set; }
}