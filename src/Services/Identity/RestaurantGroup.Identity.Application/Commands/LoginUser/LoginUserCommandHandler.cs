using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using RestaurantGroup.Identity.Application.DTOs;
using RestaurantGroup.Identity.Domain.Exceptions;
using RestaurantGroup.Identity.Domain.Interfaces;
using RestaurantGroup.Identity.Infrastructure.Security;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestaurantGroup.Identity.Application.Exceptions;

namespace RestaurantGroup.Identity.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResultDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            // Verify password
            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new InvalidCredentialsException();
            }

            // Check if user is active
            if (!user.IsActive)
            {
                throw new UserNotActiveException(user.Email);
            }

            // Update last login time
            user.UpdateLoginTime();
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            // Get user roles
            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            // Return authentication result
            return new AuthResultDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = token,
                ExpiresIn = _jwtSettings.ExpiryMinutes * 60
            };
        }
    }
}