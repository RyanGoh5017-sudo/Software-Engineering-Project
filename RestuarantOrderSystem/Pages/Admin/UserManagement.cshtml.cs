using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class UserManagementModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UserManagementModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ApplicationUser> Users { get; set; } = new();

        [BindProperty]
        public ApplicationUser NewUser { get; set; } = new();

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateUserAsync()
        {
            NewUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword("DefaultPass123!");
            _context.Users.Add(NewUser);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}