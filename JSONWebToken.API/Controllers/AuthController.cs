using Microsoft.AspNetCore.Mvc;
using JSONWebToken.Business.Abstract;
using JSONWebToken.Business.DTO.UserDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace JSONWebToken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        private string GenerateToken(UserLoginDto loginDto, string userRole)
        {
            var securekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securekey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginDto.Email),
                new Claim(ClaimTypes.Role, userRole)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _authService.Login(loginDto);

            if (user == null)
            {
                return Unauthorized();
            }

            var userRole = await _authService.GetUserRoleByEmail(loginDto.Email);

            var token = GenerateToken(loginDto, userRole);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserToAddDto registerDto)
        {
            var registeredUser = await _authService.Register(registerDto);

            if (registeredUser == null)
            {
                return BadRequest("User with this email already exists.");
            }
            return Ok(registeredUser);
        }
    }
}