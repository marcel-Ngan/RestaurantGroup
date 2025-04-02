using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantGroup.Identity.Application.Queries.GetUser;
using System;
using System.Threading.Tasks;

namespace RestaurantGroup.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets a specific user by ID
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <returns>User details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var query = new GetUserQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Gets the current user based on the JWT token
        /// </summary>
        /// <returns>Current user details</returns>
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Get the user ID from the claims provided by the JWT token
            var userIdClaim = User.FindFirst("sub")?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var query = new GetUserQuery(userId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}