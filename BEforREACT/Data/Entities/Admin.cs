using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class Admin
    {
        [Key]
        public Guid AdminID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeleteAt { get; set; } = null;
    }
}
