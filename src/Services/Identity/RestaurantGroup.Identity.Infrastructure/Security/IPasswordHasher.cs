﻿namespace RestaurantGroup.Identity.Infrastructure.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}