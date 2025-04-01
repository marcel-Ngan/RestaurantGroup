using System;

namespace RestaurantGroup.Identity.Domain
{
    /// <summary>
    /// Constants for system roles
    /// </summary>
    public static class RoleConstants
    {
        // Role Names
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Chef = "Chef";
        public const string Accountant = "Accountant";
        
        // Role IDs
        public static readonly Guid AdminId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
        public static readonly Guid ManagerId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DD");
        public static readonly Guid ChefId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DE");
        public static readonly Guid AccountantId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DF");
    }
}