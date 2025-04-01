using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantGroup.Identity.Domain;
using RestaurantGroup.Identity.Domain.Entities;

namespace RestaurantGroup.Identity.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder.Property(r => r.Description)
                .HasMaxLength(255);

            // Seed default roles using anonymous objects with constants
            builder.HasData(
                new 
                {
                    Id = RoleConstants.AdminId,
                    Name = RoleConstants.Admin, 
                    Description = "System administrator"
                },
                new 
                {
                    Id = RoleConstants.ManagerId,
                    Name = RoleConstants.Manager, 
                    Description = "Restaurant manager"
                },
                new 
                {
                    Id = RoleConstants.ChefId,
                    Name = RoleConstants.Chef, 
                    Description = "Head chef with recipe management access"
                },
                new 
                {
                    Id = RoleConstants.AccountantId,
                    Name = RoleConstants.Accountant, 
                    Description = "Accounting staff"
                }
            );
        }
    }
}