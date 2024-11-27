using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class ProductDetail
    {
        [Key]
        public Guid DetailID { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? BrandID { get; set; }
        public decimal Price { get; set; }
        public string detailDes { get; set; }

        [Range(0, 5)] // Giới hạn giá trị từ 0 đến 5
        public float Rating { get; set; } = 0; // Đánh giá sao (mặc định 0)
        public int Stock { get; set; }
    }
}
