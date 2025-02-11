using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;
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
            var users = _usersService.GetUsers();
            return Ok(users);
        }

        public IActionResult saveUserDetails(ContactMeInfo contactInfo)
        {
            bool isSaved = _usersService.saveUserDetails(contactInfo);

            if (isSaved)
            {
                return Ok(new { message = "Contact saved successfully!" });
            }
            else
            {
                return StatusCode(500, new { message = "Error saving contact." });
            }
        }

        }
}
