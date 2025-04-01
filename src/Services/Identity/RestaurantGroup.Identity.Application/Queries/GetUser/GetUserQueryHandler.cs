using AutoMapper;
using MediatR;
using RestaurantGroup.Identity.Application.DTOs;
using RestaurantGroup.Identity.Domain.Exceptions;
using RestaurantGroup.Identity.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantGroup.Identity.Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            
            if (user == null)
            {
                throw new UserNotFoundException(request.Id);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}