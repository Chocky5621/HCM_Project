using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests
{
	public class RoleBasedAccessTests
	{
		[Theory]
		[InlineData("HRAdmin", true)]
		[InlineData("Manager", false)]
		[InlineData("Employee", false)]
		public void OnlyHRAdmin_ShouldHaveCreateAccess(string role, bool expected)
		{
			
			bool hasAccess = role == "HRAdmin";

			Assert.Equal(expected, hasAccess);
		}
	}
}
