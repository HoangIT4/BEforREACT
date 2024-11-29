namespace BEforREACT.DTOs
{
    public class ProductsDetailDTO
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public string PreImg { get; set; }
        public string detailDes { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public float Rating { get; set; }

        public BrandDTO Brands { get; set; }
        public List<CategoryDTO> Categories { get; set; }

        public string FormattedPrice => Price.ToString("#,0.###") + " " + "đ";

    }

}
