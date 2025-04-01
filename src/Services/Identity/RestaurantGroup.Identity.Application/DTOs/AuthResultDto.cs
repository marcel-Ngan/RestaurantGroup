namespace RestaurantGroup.Identity.Application.DTOs;

public class AuthResultDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
    public int ExpiresIn { get; set; } // Token expiration in seconds
}