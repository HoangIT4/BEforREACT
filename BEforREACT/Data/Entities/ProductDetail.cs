using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class ProductDetail
    {
        [Key]
        public Guid DetailID { get; set; }

        [Required]
        public Guid ProductID { get; set; }


        public decimal Price { get; set; }
        public string? detailDes { get; set; }

        public bool IsHotDeal { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsBestSeller { get; set; } = false;

        //public virtual Brand Brand { get; set; }

        [Range(0, 5)] // Giới hạn giá trị từ 0 đến 5
        public float Rating { get; set; } = 0; // Đánh giá sao (mặc định 0)
        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
