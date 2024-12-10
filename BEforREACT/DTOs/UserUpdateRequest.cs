﻿using BEforREACT.Data.Entities;

namespace BEforREACT.DTOs
{
    public class UserUpdateRequest
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        //public string? Password { get; set; }
        public string? Address { get; set; }
        public string? BirthDay { get; set; }
        public string Gender { get; set; } = GenderEnum.Male.ToString();
        public DateTime? updatedAt { get; set; } = DateTime.UtcNow;
    }
}
