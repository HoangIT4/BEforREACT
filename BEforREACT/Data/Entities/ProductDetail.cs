using System.ComponentModel.DataAnnotations;

public class ProductDetail
{
    [Key]
    public Guid DetailID { get; set; }

    [Required]
    public Guid ProductID { get; set; }

    public decimal Price { get; set; }
    public string? detailDes { get; set; }
    public string? Description { get; set; }

    public bool IsHotDeal { get; set; } = false;
    public bool IsNew { get; set; } = false;
    public bool IsBestSeller { get; set; } = false;

    //[Range(0, 5)] 
    //public float Rating { get; set; } = 0;
    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;

    public virtual Product Product { get; set; } // Thêm virtual cho phép lazy loading nếu cần
}