using System;
using System.Collections.Generic;

namespace RestaurantGroup.Identity.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PasswordHash { get; private set; }
        public bool EmailVerified { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        // For EF Core
        protected User() { }

        public User(string email, string firstName, string lastName, string passwordHash)
        {
            Id = Guid.NewGuid();
            Email = email?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(email));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            EmailVerified = false;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void VerifyEmail()
        {
            EmailVerified = true;
        }

        public void UpdateLoginTime()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        public void Update(string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public void UpdatePassword(string passwordHash)
        {
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void AddRole(Role role)
        {
            UserRoles.Add(new UserRole(this.Id, role.Id));
        }
    }
}