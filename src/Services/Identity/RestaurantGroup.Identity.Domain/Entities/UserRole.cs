using System;

namespace RestaurantGroup.Identity.Domain.Entities
{
    public class UserRole
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; }
        public DateTime AssignedAt { get; private set; }

        // For EF Core
        protected UserRole() { }

        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
            AssignedAt = DateTime.UtcNow;
        }
    }
}