using MediatR;
using RestaurantGroup.Identity.Application.DTOs;
using System.Collections.Generic;

namespace RestaurantGroup.Identity.Application.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}