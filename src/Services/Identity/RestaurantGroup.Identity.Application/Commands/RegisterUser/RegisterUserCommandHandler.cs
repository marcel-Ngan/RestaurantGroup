using AutoMapper;
using MediatR;
using RestaurantGroup.Identity.Application.DTOs;
using RestaurantGroup.Identity.Domain.Entities;
using RestaurantGroup.Identity.Domain.Exceptions;
using RestaurantGroup.Identity.Domain.Interfaces;
using RestaurantGroup.Identity.Infrastructure.Security;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantGroup.Identity.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user already exists
            if (await _userRepository.ExistsByEmailAsync(request.Email))
            {
                throw new UserAlreadyExistsException(request.Email);
            }

            // Hash password
            var passwordHash = _passwordHasher.HashPassword(request.Password);

            // Create user
            var user = new User(request.Email, request.FirstName, request.LastName, passwordHash);

            // Assign roles
            if (request.Roles != null && request.Roles.Any())
            {
                foreach (var roleName in request.Roles)
                {
                    var role = await _roleRepository.GetByNameAsync(roleName);
                    if (role != null)
                    {
                        user.AddRole(role);
                    }
                    else
                    {
                        throw new RoleNotFoundException(roleName);
                    }
                }
            }
            else
            {
                // If no roles are specified, assign a default role
                // You might want to define a default role constant
                var defaultRole = await _roleRepository.GetByNameAsync("User");
                if (defaultRole != null)
                {
                    user.AddRole(defaultRole);
                }
            }

            // Save user
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // Return user DTO
            return _mapper.Map<UserDto>(user);
        }
    }
}