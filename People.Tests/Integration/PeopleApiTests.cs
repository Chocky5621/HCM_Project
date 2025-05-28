using People.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests.Integration
{
    public class PeopleApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PeopleApiTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task HRAdmin_Can_Create_New_Person()
        {
            // Arrange
            var person = new Person
            {
                Id = 100,
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com",
                JobTitle = "QA Engineer",
                Salary = 3000,
                Department = "IT",
                Username = "testuser"
            };

            _client.DefaultRequestHeaders.Add("Authorization", "mock-token-petar-HRAdmin");

            var response = await _client.PostAsJsonAsync("/api/people", person);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PutPerson_shouldAllowManager_ForSameDepartment()
        {
            var updatedPerson = new
            {
                id = 1,
                firstName = "Martin",
                lastName = "Mirchev",
                email = "mirchev@mhp.com",
                jobTitle = "Senior Engineer",
                salary = 4000,
                department = "IT",
                username = "martin"
            };

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "mock-token-martin-Manager");

            
            var response = await _client.PutAsJsonAsync("/api/people/1", updatedPerson);

            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response content: " + responseContent);
        }

    }
}
