using RestaurantGroup.Identity.Domain.Entities;
using System.Collections.Generic;

namespace RestaurantGroup.Identity.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IEnumerable<string> roles);
    }
}