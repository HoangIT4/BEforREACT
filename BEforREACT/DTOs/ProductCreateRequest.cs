namespace BEforREACT.DTOs
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }

        public string? Src { get; set; }
        public string? PreImg { get; set; }
        public Guid BrandID { get; set; }
        public List<Guid>? CategoryIDs { get; set; } // Danh sách các Category liên quan
        public List<ProductDetailCreateRequest>? ProductDetailsRequest { get; set; }

    }
    public class ProductDetailCreateRequest
    {
        public decimal? Price { get; set; }
        public int Stock { get; set; }
        public bool IsHotDeal { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsBestSeller { get; set; } = false;
        public string? Weight { get; set; }
        public string? Origin { get; set; }
        public string? Description { get; set; }
        public string? detailDes { get; set; }

    }
}
