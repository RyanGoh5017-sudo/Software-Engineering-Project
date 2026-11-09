using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Staff
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUser StaffUser { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                StaffUser = await _context.Users.FindAsync(int.Parse(userIdClaim)) ?? new ApplicationUser();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Users.Update(StaffUser);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}