using Auth.API.MockRepos;
using Auth.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginRequest loginRequest)
		{
			var user = UserRepository.ValidateUser(loginRequest.Username, loginRequest.Password);
			if (user == null)
				return Unauthorized("Invalid username or password");

			return Ok(new
			{
				token = $"mock-token-{user.Username}-{user.Role}",
				username = user.Username,
				role = user.Role,
				department = user.Department
			});
		}
	}
	
}
