using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestuarantOrderSystem.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin")) return RedirectToPage("/Admin/Dashboard");
                if (User.IsInRole("Staff")) return RedirectToPage("/Staff/Dashboard");
                if (User.IsInRole("Customer")) return RedirectToPage("/Customer/Dashboard");
            }
            return Page();
        }
    }
}