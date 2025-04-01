using MediatR;
using RestaurantGroup.Identity.Application.DTOs;

namespace RestaurantGroup.Identity.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}