using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using People.Web.Models;

namespace People.Web.Pages
{
    public class EditPersonModel : PageModel
    {
        [BindProperty] public Person Person { get; set; } = new();
        public string? Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("role");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("Index");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.GetAsync($"http://localhost:5021/api/people/{id}");

            if (!response.IsSuccessStatusCode)
                return Content("Cannot load person.");

            Person = await response.Content.ReadFromJsonAsync<Person>();

            if (role == "Manager")
            {
                var username = token.Replace("mock-token-", "").Split('-')[0];
                if (Person.Department != GetManagerDepartment(username))
                    return Content("You cannot edit people outside your department.");
            }
            else if (role != "HRAdmin")
            {
                return Content("You don't have permission to edit this person.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("Index");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PutAsJsonAsync($"http://localhost:5021/api/people/{Person.Id}", Person);

            Message = response.IsSuccessStatusCode
                ? "Person updated successfully!"
                : "Failed to update person.";

            return Page();
        }

        private string GetManagerDepartment(string username)
        {
            return username == "martin" ? "IT" : "Unknown";
        }
    }
}
