using Microsoft.AspNetCore.Mvc;
using People.API.Data;
using People.API.Models;

namespace People.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PeopleController : ControllerBase
	{
		private readonly AppDbContext _context;

		public PeopleController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetPeople()
		{
            
            var token = Request.Headers["Authorization"].ToString().Trim();

            if (!token.StartsWith("mock-token-"))
				return Unauthorized("Invalid token");

			var parts = token.Replace("mock-token-", "").Split('-');
			var username = parts[0];
			var role = parts[1];

			if (role == "HRAdmin")
			{
				return Ok(_context.People.ToList());
			}
			else if (role == "Manager")
			{
				var department = _context.People.FirstOrDefault(p => p.Username == username)?.Department;
				if (department == null)
					return NotFound("Manager not found");

				var peopleInDepartment = _context.People.Where(p => p.Department == department).ToList();
				return Ok(peopleInDepartment);
			}
			else if (role == "Employee")
			{
				var person = _context.People.FirstOrDefault(p => p.Username == username);
				if (person == null)
					return NotFound("Employee not found");

				return Ok(person);
			}

			return Forbid("Unknown role");
		}
        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            var token = Request.Headers["Authorization"].ToString();
            if (!token.StartsWith("mock-token-"))
                return Unauthorized("Invalid token");

            var parts = token.Replace("mock-token-", "").Split('-');
            var username = parts[0];
            var role = parts[1];

            var person = _context.People.FirstOrDefault(p => p.Id == id);
            if (person == null)
                return NotFound("Person not found");

            if (role == "HRAdmin")
            {
                return Ok(person);
            }

            if (role == "Manager")
            {
                var manager = _context.People.FirstOrDefault(p => p.Username == username);
                if (manager == null || person.Department != manager.Department)
                    return Forbid("You cannot access people outside your department.");

                return Ok(person);
            }

            if (role == "Employee")
            {
                if (person.Username != username)
                    return Forbid("You can only view your own data.");

                return Ok(person);
            }

            return Forbid("Invalid role.");
        }
        [HttpPost]
		public IActionResult CreatePerson([FromBody] Person person)
		{
            if (_context.People.Any(p => p.Email == person.Email || p.Username == person.Username))
            {
                return BadRequest("User with same email or username already exists.");
            }
            var token = Request.Headers["Authorization"].ToString();
			if (!token.StartsWith("mock-token-")) return Unauthorized("Invalid token");

			var role = token.Replace("mock-token-", "").Split('-')[1];

			if (role != "HRAdmin")
				return Forbid("Only HRAdmin can create new people");

			_context.People.Add(person);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetPeople), new { id = person.Id }, person);
		}

		[HttpPut("{id}")]
		public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
		{
			var token = Request.Headers["Authorization"].ToString();
			if (!token.StartsWith("mock-token-"))
				return Unauthorized("Invalid token");

			var parts = token.Replace("mock-token-", "").Split('-');
			var username = parts[0];
			var role = parts[1];

			var person = _context.People.FirstOrDefault(p => p.Id == id);
			if (person == null)
				return NotFound("Person not found");

			
			if (role == "HRAdmin")
			{
				UpdateFields(person, updatedPerson);
				_context.SaveChanges();
				return Ok(person);
			}

			if (role == "Manager")
			{
				var manager = _context.People.FirstOrDefault(p => p.Username == username);
				if (manager == null || person.Department != manager.Department)
					return Forbid("You can only edit people from your department.");

				UpdateFields(person, updatedPerson);
				_context.SaveChanges();
				return Ok(person);
			}

			return Forbid("You don't have permission to edit this person.");
		}

		private void UpdateFields(Person existing, Person updated)
		{
			existing.FirstName = updated.FirstName;
			existing.LastName = updated.LastName;
			existing.Email = updated.Email;
			existing.JobTitle = updated.JobTitle;
			existing.Salary = updated.Salary;
			existing.Department = updated.Department;
			existing.Username = updated.Username;
		}

		[HttpDelete("{id}")]
		public IActionResult DeletePerson(int id)
		{
			var token = Request.Headers["Authorization"].ToString();
			if (!token.StartsWith("mock-token-"))
				return Unauthorized("Invalid token");

			var parts = token.Replace("mock-token-", "").Split('-');
			var username = parts[0];
			var role = parts[1];

			var person = _context.People.FirstOrDefault(p => p.Id == id);
			if (person == null)
				return NotFound("Person not found");

			if (role == "HRAdmin")
			{
				_context.People.Remove(person);
				_context.SaveChanges();
				return Ok("Person deleted");
			}

			if (role == "Manager")
			{
				var manager = _context.People.FirstOrDefault(p => p.Username == username);
				if (manager == null || person.Department != manager.Department)
					return Forbid("You can only delete people from your department");

				_context.People.Remove(person);
				_context.SaveChanges();
				return Ok("Person deleted");
			}

			return Forbid("You don't have permission to delete this person");
		}
	}
}
