namespace Auth.API.Models
{
	public class User
	{
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string Role { get; set; } = null!;
		public string Department { get; set; } = null!;
	}
}
