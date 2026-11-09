using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal TodaySales { get; set; }
        public int TotalOrdersToday { get; set; }
        public double OccupancyRate { get; set; }
        public double AverageRating { get; set; }

        public async Task OnGetAsync()
        {
            var today = DateTime.UtcNow.Date;

            TodaySales = await _context.Orders
                .Where(o => o.OrderDate >= today && o.Status == OrderStatus.Completed)
                .SumAsync(o => o.TotalAmount);

            TotalOrdersToday = await _context.Orders
                .Where(o => o.OrderDate >= today)
                .CountAsync();

            int totalTables = await _context.Tables.CountAsync();
            int occupiedTables = await _context.Tables.CountAsync(t => t.Status == TableStatus.Occupied);
            OccupancyRate = totalTables > 0 ? (double)occupiedTables / totalTables * 100 : 0;

            AverageRating = await _context.Feedbacks.AnyAsync()
                ? await _context.Feedbacks.AverageAsync(f => f.Rating)
                : 5.0;
        }
    }
}