using Microsoft.EntityFrameworkCore;
using RestaurantGroup.Identity.Domain.Entities;
using RestaurantGroup.Identity.Domain.Interfaces;
using RestaurantGroup.Identity.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantGroup.Identity.Infrastructure.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IdentityDbContext _context;

        public RoleRepository(IdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}