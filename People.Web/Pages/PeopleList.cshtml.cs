using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System;
using People.Web.Models;


namespace People.Web.Pages
{
    public class PeopleListModel : PageModel
    {
        public List<Person> People { get; set; } = new();
        public string Role { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("role");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("Index");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.GetAsync("http://localhost:5021/api/people");

            if (!response.IsSuccessStatusCode)
            {
                People = new List<Person>();
                return Page();
            }

            if (role == "Employee")
            {
                var person = await response.Content.ReadFromJsonAsync<Person>();
                People = new List<Person> { person }; 
            }
            else
            {
                People = await response.Content.ReadFromJsonAsync<List<Person>>();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token)) return RedirectToPage("Index");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.DeleteAsync($"http://localhost:5021/api/people/{id}");
            return RedirectToPage();
        }


    }

}
