namespace BEforREACT.DTOs
{
    public class CategoryDTO
    {
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
