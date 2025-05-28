using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace People.Web.Pages
{
    public class DashboardModel : PageModel
    {
        public string Role { get; set; }

        public IActionResult OnGet()
        {
            Role = HttpContext.Session.GetString("role");

            if (string.IsNullOrEmpty(Role))
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
