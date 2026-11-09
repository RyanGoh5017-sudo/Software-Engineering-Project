using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Customer
{
    public class FeedbackModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FeedbackModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Feedback NewFeedback { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return RedirectToPage("/Customer/Login");

            NewFeedback.UserId = int.Parse(userIdClaim);
            NewFeedback.SubmittedAt = DateTime.UtcNow;

            _context.Feedbacks.Add(NewFeedback);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Customer/Dashboard");
        }
    }
}