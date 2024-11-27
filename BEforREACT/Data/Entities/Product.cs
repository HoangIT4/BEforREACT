using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        public Guid DetailID { get; set; }
        public string Name { get; set; }

        public string Src { get; set; }
        public string PreImg { get; set; }
        public string Description { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
