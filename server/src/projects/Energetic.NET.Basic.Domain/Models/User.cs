﻿namespace Energetic.NET.Basic.Domain.Models
{
    public class User : BaseAuditableEntity
    {
        public User(string userName) : base()
        {
            UserName = userName;
        }

        public string UserName { get; init; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
