using System.ComponentModel.DataAnnotations;

namespace RestaurantGroup.Identity.API.Models;

/// <summary>
/// Request model for user registration
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Email address of the user (will be used for login)
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Password for the account
    /// </summary>
    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    /// <summary>
    /// First name of the user
    /// </summary>
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name of the user
    /// </summary>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Roles to assign to the user
    /// </summary>
    public List<string> Roles { get; set; } = new List<string>();
}