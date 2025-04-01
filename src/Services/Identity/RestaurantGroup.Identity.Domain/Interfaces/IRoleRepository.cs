using RestaurantGroup.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantGroup.Identity.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<Role> GetByNameAsync(string name);
        Task<IEnumerable<Role>> GetAllAsync();
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task SaveChangesAsync();
    }
}