using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Services;

namespace MyPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UsersService _usersService;

        public UserController(UsersService userService)
        {
            _usersService = userService;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            //eturn Ok("API is Running");
            var users = _usersService.GetUsers();
            return Ok(users);
        }
    }
}
