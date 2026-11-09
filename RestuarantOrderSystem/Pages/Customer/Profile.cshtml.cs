using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Customer
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUser CustomerUser { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                CustomerUser = await _context.Users.FindAsync(int.Parse(userIdClaim)) ?? new ApplicationUser();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Users.Update(CustomerUser);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}