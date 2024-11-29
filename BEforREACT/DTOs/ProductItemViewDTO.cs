namespace BEforREACT.DTOs
{
    public class ProductItemViewDTO
    {
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string Src { get; set; }
        public string PreImg { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string FormattedPrice => Price.ToString("#,0.###") + " " + "đ";
    }
}
