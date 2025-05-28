using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using People.Web.Models;

namespace People.Web.Pages
{
    public class CreatePersonModel : PageModel
    {
        [BindProperty] public Person NewPerson { get; set; } = new();
        public string? Message { get; set; }
        [BindProperty]
        public string Role { get; set; }
        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "HRAdmin")
            {
                return Content("Only HRAdmin can access this page.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token)) return RedirectToPage("Index");
            NewPerson.Role = Role;
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsJsonAsync("http://localhost:5021/api/people", NewPerson);

            if (response.IsSuccessStatusCode)
            {
                Message = "Person created successfully!";
            }
            else
            {
                Message = "Failed to create person.";
            }

            return Page();
        }
    }
}
