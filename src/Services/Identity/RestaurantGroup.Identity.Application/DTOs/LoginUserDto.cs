using System;
using System.Collections.Generic;

namespace RestaurantGroup.Identity.Application.DTOs
{
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}