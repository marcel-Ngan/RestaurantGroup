using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantGroup.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetPublic()
        {
            return Ok(new { message = "This is a public endpoint. No authentication required." });
        }
        
        [HttpGet("authenticated")]
        [Authorize]
        public IActionResult GetAuthenticated()
        {
            return Ok(new { message = "You are authenticated! This endpoint is protected." });
        }
        
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdmin()
        {
            return Ok(new { message = "You are authorized as an Admin!" });
        }
    }
}