using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthAPI.Data;

namespace SimpleAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUSers()
        {
            return Ok(new { Users = FakeUserStorage.Users });
        }
        [HttpGet("LetMeIn")]
        public IActionResult LetMeIn()
        {
            return Ok("welcome, you are authorized here!");
        }
    }
}
