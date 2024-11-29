namespace BEforREACT.DTOs
{
    public class ProductQuerryParams
    {
        public Guid BrandID { get; set; }
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public bool IsHotDeal { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsBestSeller { get; set; } = false;
    }
}
