using RestaurantGroup.Blazor.Models.Users;

namespace RestaurantGroup.Blazor.Models.Authentication
{
    public class AuthResult
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}