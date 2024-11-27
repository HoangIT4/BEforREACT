namespace BEforREACT.DTOs
{
    public class ProductsDTO
    {
        public Guid ProductID { get; set; }

        public Guid DetailID { get; set; }
        //public string BrandName { get; set; }
        //public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public string PreImg { get; set; }
        public string detailDes { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public float Rating { get; set; }

        public string FormattedPrice => Price.ToString("#,0.###") + " " + "đ";

    }

}
