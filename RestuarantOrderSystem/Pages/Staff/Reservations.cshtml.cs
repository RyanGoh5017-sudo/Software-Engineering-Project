using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Staff
{
    public class ReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Reservation> Reservations { get; set; } = new();

        public async Task OnGetAsync()
        {
            Reservations = await _context.Reservations.Include(r => r.User).Include(r => r.Table).ToListAsync();
        }

        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            var res = await _context.Reservations.FindAsync(id);
            if (res != null) { res.Status = ReservationStatus.Confirmed; await _context.SaveChangesAsync(); }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var res = await _context.Reservations.FindAsync(id);
            if (res != null) { res.Status = ReservationStatus.Cancelled; await _context.SaveChangesAsync(); }
            return RedirectToPage();
        }
    }
}