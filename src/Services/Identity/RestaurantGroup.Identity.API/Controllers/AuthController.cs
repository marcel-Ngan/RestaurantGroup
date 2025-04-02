using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantGroup.Identity.API.Models;
using RestaurantGroup.Identity.Application.Commands.LoginUser;
using RestaurantGroup.Identity.Application.Commands.RegisterUser;
using RestaurantGroup.Identity.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace RestaurantGroup.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="request">User registration information</param>
        /// <returns>Created user details</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = new RegisterUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Roles = request.Roles
            };

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>Authentication result with JWT token</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginUserCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        // This endpoint is referenced by the register endpoint
        // to create a location header for the newly created user
        [HttpGet("user/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return Ok();
        }
    }
}