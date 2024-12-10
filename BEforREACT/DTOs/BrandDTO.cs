namespace BEforREACT.DTOs
{
    public class BrandDTO
    {
        public Guid BrandID { get; set; }
        public string? BrandName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
