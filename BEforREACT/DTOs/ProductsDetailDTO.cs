﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BEforREACT.DTOs
{
    public class ProductsDetailDTO
    {
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string? Src { get; set; }
        public string? PreImg { get; set; }
        public string? detailDes { get; set; }
        public bool IsHotDeal { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsBestSeller { get; set; } = false;
        public string? Weight { get; set; }
        public string? Origin { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public float Rating { get; set; }

        public BrandDTO Brands { get; set; }
        public List<CategoryDTO> Categories { get; set; }  // Danh sách các category

        //public string FormattedPrice => Price.ToString("#,0.###") + " " + "đ";

    }

}
