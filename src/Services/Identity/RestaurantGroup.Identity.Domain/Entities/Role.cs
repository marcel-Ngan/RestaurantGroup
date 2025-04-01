using System;
using System.Collections.Generic;

namespace RestaurantGroup.Identity.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        // For EF Core
        protected Role() { }

        public Role(string name, string description = null)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }
    }
}