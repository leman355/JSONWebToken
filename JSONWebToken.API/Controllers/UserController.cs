using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using JSONWebToken.Business.Abstract;
using JSONWebToken.Business.DTO.UserDtos;
using Microsoft.AspNetCore.Authorization;

namespace JSONWebToken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get all users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }


        [Authorize(Policy = "AdminRole")]
        [HttpGet("{userId}")]
        [SwaggerOperation(Summary = "Get a user by ID")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Policy = "UserRoles")]
        [HttpGet("role/{roleId}")]
        [SwaggerOperation(Summary = "Get users by role ID")]
        public async Task<IActionResult> GetUsersByRoleId(int roleId)
        {
            var users = await _userService.GetUsersByRoleId(roleId);
            return Ok(users);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new user")]
        public async Task<IActionResult> CreateUser([FromBody] UserToAddDto dto)
        {
            var user = await _userService.CreateUser(dto);
            return Ok(user);
        }


        [HttpPut("{userId}")]
        [SwaggerOperation(Summary = "Update a user")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserToUpdateDto dto)
        {
            var updatedUser = await _userService.UpdateUser(userId, dto);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{userId}")]
        [SwaggerOperation(Summary = "Delete a user by ID")]
        public async Task<IActionResult> DeleteUserById(int userId)
        {
            await _userService.DeleteUserById(userId);
            return NoContent();
        }

        [HttpGet("roleByEmail")]
        [SwaggerOperation(Summary = "Get user role by email")]
        public async Task<IActionResult> GetUserRoleByEmail([FromQuery] string email)
        {
            var role = await _userService.GetUserRoleByEmail(email);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }
    }
}
