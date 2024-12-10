using System.ComponentModel.DataAnnotations;

namespace BEforREACT.Data.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = UserRoles.User;
        public string? UserName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        public string Gender { get; set; } = GenderEnum.Male.ToString();
        public DateTime? Birthday { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = null;
        public DateTime? DeletedAt { get; set; } = null;

    }
    public enum GenderEnum
    {
        Male,
        Female,
        Other
    }
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

}
