using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Pages.Customer
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string ErrorMessage { get; set; } = string.Empty;

        public class InputModel
        {
            [Required]
            public string FullName { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required, Phone]
            public string PhoneNumber { get; set; } = string.Empty;

            [Required, MinLength(6)]
            public string Password { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email == Input.Email);
            if (existing != null)
            {
                ErrorMessage = "Email is already registered.";
                return Page();
            }

            var newUser = new ApplicationUser
            {
                FullName = Input.FullName,
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Input.Password),
                Role = UserRole.Customer
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Customer/Login");
        }
    }
}