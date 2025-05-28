using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using People.Web.Models;
using System.Text.Json;

namespace People.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            using var client = new HttpClient();

            var response = await client.PostAsJsonAsync("http://localhost:5218/api/auth/login",
                new { username = Username, password = Password });

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = "Invalid login.";
                return Page();
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            HttpContext.Session.SetString("token", result.Token);
            HttpContext.Session.SetString("role", result.Role);

            return RedirectToPage("Dashboard");
        }
    }
}
