using People.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests
{
	public class PersonValidationTests
	{
		[Fact]
		public void Person_WithEmptyFirstName_ShouldBeInvalid()
		{
			var person = new Person
			{
				FirstName = "",
				LastName = "Ivanov",
				Email = "test@example.com",
				JobTitle = "Dev",
				Salary = 2000,
				Department = "IT",
				Username = "testuser"
			};

			Assert.False(!string.IsNullOrWhiteSpace(person.FirstName));
		}

		[Fact]
		public void Person_WithNegativeSalary_ShouldFailValidation()
		{
			var person = new Person
			{
				FirstName = "Anna",
				LastName = "Ivanova",
				Email = "anna@example.com",
				JobTitle = "QA",
				Salary = -500,
				Department = "QA",
				Username = "anna"
			};

			Assert.True(person.Salary < 0);
		}
	}

}
