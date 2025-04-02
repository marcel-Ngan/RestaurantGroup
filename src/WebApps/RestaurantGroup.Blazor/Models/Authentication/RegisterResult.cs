using RestaurantGroup.Blazor.Models.Users;

namespace RestaurantGroup.Blazor.Models.Authentication
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public UserModel User { get; set; }
        public string Error { get; set; }
    }
}