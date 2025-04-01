namespace RestaurantGroup.Identity.Application.DTOs;

public class RegisterUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}