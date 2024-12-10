using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductDetail
{
    [Key]
    public Guid DetailID { get; set; }

    [Required]
    public Guid ProductID { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Price { get; set; }
    public string? detailDes { get; set; }
    public string? Description { get; set; }

    public string? Weight { get; set; }
    public string? Origin { get; set; }

    public bool IsHotDeal { get; set; } = false;
    public bool IsNew { get; set; } = false;
    public bool IsBestSeller { get; set; } = false;

    //[Range(0, 5)] 
    //public float Rating { get; set; } = 0;
    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;

    public virtual Product? Product { get; set; } // Thêm virtual cho phép lazy loading nếu cần
}