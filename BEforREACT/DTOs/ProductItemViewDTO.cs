using System.ComponentModel.DataAnnotations.Schema;

namespace BEforREACT.DTOs
{
    public class ProductItemViewDTO
    {
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string? Src { get; set; }
        public string? PreImg { get; set; }
        public string? Description { get; set; }
        public bool IsHotDeal { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsBestSeller { get; set; } = false;

        [Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public BrandDTO Brands { get; set; }
        public List<string>? Categories { get; set; }
        public string FormattedPrice => Price.ToString("0.000");
    }
}
