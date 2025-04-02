using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantGroup.Identity.API.Models
{
    /// <summary>
    /// Request model for user login
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Email address of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password for the account
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}