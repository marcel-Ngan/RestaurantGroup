using RestaurantGroup.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantGroup.Identity.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task SaveChangesAsync();
    }
}