using People.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests
{
	public class UpdateFields
	{
		[Fact]
		public void UpdateFields_ShouldUpdateAllValues()
		{
			var original = new Person
			{
				Id = 1,
				FirstName = "John",
				LastName = "Doe",
				Email = "old@mail.com",
				JobTitle = "Junior",
				Salary = 2000,
				Department = "OldDept",
				Username = "john"
			};

			var updated = new Person
			{
				Id = 1,
				FirstName = "Jane",
				LastName = "Smith",
				Email = "new@mail.com",
				JobTitle = "Senior",
				Salary = 3000,
				Department = "NewDept",
				Username = "jane"
			};

			original.FirstName = updated.FirstName;
			original.LastName = updated.LastName;
			original.Email = updated.Email;
			original.JobTitle = updated.JobTitle;
			original.Salary = updated.Salary;
			original.Department = updated.Department;
			original.Username = updated.Username;

			Assert.Equal("Jane", original.FirstName);
			Assert.Equal("Smith", original.LastName);
			Assert.Equal("new@mail.com", original.Email);
			Assert.Equal("Senior", original.JobTitle);
			Assert.Equal(3000, original.Salary);
			Assert.Equal("NewDept", original.Department);
			Assert.Equal("jane", original.Username);
		}
	}
}
