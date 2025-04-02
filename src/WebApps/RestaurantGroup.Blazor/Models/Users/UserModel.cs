using System;
using System.Collections.Generic;

namespace RestaurantGroup.Blazor.Models.Users
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}