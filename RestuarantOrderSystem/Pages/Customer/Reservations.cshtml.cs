using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Customer
{
    public class ReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<RestaurantTable> AvailableTables { get; set; } = new();

        [BindProperty]
        public ReservationInput Input { get; set; } = new();

        public class ReservationInput
        {
            [Required]
            public DateTime ReservationDate { get; set; } = DateTime.Today;

            [Required]
            public TimeSpan TimeSlot { get; set; }

            [Range(1, 20)]
            public int GuestCount { get; set; } = 2;

            public int TableId { get; set; }

            public string? SpecialRequests { get; set; }
        }

        public async Task OnGetAsync()
        {
            AvailableTables = await _context.Tables.Where(t => t.Status == TableStatus.Available).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return RedirectToPage("/Customer/Login");

            // Ensure SpecialRequests is never null if left blank by the user
            Input.SpecialRequests ??= string.Empty;

            if (!ModelState.IsValid)
            {
                AvailableTables = await _context.Tables.Where(t => t.Status == TableStatus.Available).ToListAsync();
                return Page();
            }

            var reservation = new Reservation
            {
                UserId = int.Parse(userIdClaim),
                TableId = Input.TableId,
                ReservationDate = Input.ReservationDate,
                TimeSlot = Input.TimeSlot,
                GuestCount = Input.GuestCount,
                SpecialRequests = Input.SpecialRequests,
                Status = ReservationStatus.Pending
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Customer/Orders");
        }
    }
}