using Microsoft.EntityFrameworkCore;
using People.API.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace People.API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
		{
		}

		public DbSet<Person> People => Set<Person>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>().HasData(
				new Person
				{
					Id = 1,
					FirstName = "Martin",
					LastName = "Mirchev",
					Email = "mirchev@mhp.com",
					JobTitle = "Software Engineer",
					Salary = 3500,
					Department = "IT",
					Username = "martin"
				},
				new Person
				{
					Id = 2,
					FirstName = "Stefan",
					LastName = "Stoyanov",
					Email = "stoyanov@mhp.com",
					JobTitle = " Junior Software Engineer",
					Salary = 4200,
					Department = "IT",
					Username = "stefan"
				},
				new Person
				{
					Id = 3,		
					FirstName = "Petar",
					LastName = "Nenov",
					Email = "nenov@mhp.com",
					JobTitle = " HR Manager",
					Salary = 4200,
					Department = "HR",
					Username = "petar"
				}
			);
		}
	}
}
