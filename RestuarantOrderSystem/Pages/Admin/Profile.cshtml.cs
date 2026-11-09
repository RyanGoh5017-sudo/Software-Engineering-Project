using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUser AdminUser { get; set; } = new();

        [BindProperty]
        public string NewPassword { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                AdminUser = await _context.Users.FindAsync(int.Parse(userIdClaim)) ?? new ApplicationUser();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userInDb = await _context.Users.FindAsync(AdminUser.UserId);
            if (userInDb != null)
            {
                userInDb.FullName = AdminUser.FullName;
                userInDb.Email = AdminUser.Email;
                userInDb.PhoneNumber = AdminUser.PhoneNumber;

                if (!string.IsNullOrWhiteSpace(NewPassword))
                {
                    userInDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                }

                await _context.SaveChangesAsync();
                Message = "Administrator profile successfully updated.";
            }

            return Page();
        }
    }
}