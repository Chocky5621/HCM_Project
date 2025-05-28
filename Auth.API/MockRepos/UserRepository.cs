using Auth.API.Models;

namespace Auth.API.MockRepos
{
	public class UserRepository
	{
		private static List<User> Users = new()
		{
			new User { Username = "stefan", Password = "12345", Role = "Employee", Department = "IT" },
			new User { Username = "Dimitar", Password= "12345", Role ="Empoyee", Department="HR" },
			new User { Username = "martin", Password = "12345", Role = "Manager", Department = "IT" },
			new User { Username = "petar", Password = "12345", Role = "HRAdmin", Department = "HR" },
		};

		public static User? ValidateUser(string username, string password)
		{
			return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
		}
	}
}
