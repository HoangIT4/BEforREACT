﻿using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public int? Status { get; set; }


        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; } = null;
        public virtual ICollection<OrderItem> OrderItems { get; set; }


    }
}
