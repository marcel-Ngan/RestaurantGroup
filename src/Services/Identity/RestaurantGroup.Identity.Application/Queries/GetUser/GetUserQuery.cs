using MediatR;
using RestaurantGroup.Identity.Application.DTOs;
using System;

namespace RestaurantGroup.Identity.Application.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        
        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}