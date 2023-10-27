using JSONWebToken.Business.Abstract;
using JSONWebToken.Business.DTO.RoleDtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JSONWebToken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of all roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        [SwaggerOperation(Summary = "Get a role by ID")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            var role = await _roleService.GetRoleById(roleId);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleToAddDto dto)
        {
            var createdRole = await _roleService.CreateRole(dto);

            if (createdRole != null)
            {
                return Ok(createdRole);
            }
            else
            {
                return BadRequest("Failed to create role");
            }
        }

        [HttpPut("{roleId}")]
        [SwaggerOperation(Summary = "Update a role by ID")]
        public async Task<IActionResult> UpdateRole([FromRoute] int roleId, [FromBody] RoleToUpdateDto roleToUpdateDto)
        {
            if (roleId <= 0)
            {
                return BadRequest();
            }

            var updatedRole = await _roleService.UpdateRole(roleId, roleToUpdateDto);

            if (updatedRole == null)
            {
                return NotFound();
            }

            return Ok(updatedRole);
        }

        [HttpDelete("{roleId}")]
        [SwaggerOperation(Summary = "Delete a role by ID")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            await _roleService.DeleteRoleById(roleId);
            return NoContent();
        }
    }
}
