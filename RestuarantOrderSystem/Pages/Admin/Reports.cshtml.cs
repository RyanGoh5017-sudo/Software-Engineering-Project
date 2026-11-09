using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReportsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public void OnGet()
        {
            StartDate ??= DateTime.UtcNow.AddDays(-30);
            EndDate ??= DateTime.UtcNow;
        }

        public async Task<IActionResult> OnPostExportCsvAsync(DateTime? start, DateTime? end)
        {
            var filterStart = start ?? DateTime.UtcNow.AddDays(-30);
            var filterEnd = end ?? DateTime.UtcNow;

            var orders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.OrderDate >= filterStart && o.OrderDate <= filterEnd)
                .ToListAsync();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("OrderId,CustomerName,OrderDate,Status,TotalAmount");

            foreach (var o in orders)
            {
                csvBuilder.AppendLine($"{o.OrderId},\"{o.User?.FullName}\",{o.OrderDate:yyyy-MM-dd HH:mm:ss},{o.Status},{o.TotalAmount:F2}");
            }

            byte[] csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            return File(csvBytes, "text/csv", $"Orders_Export_{filterStart:yyyyMMdd}_to_{filterEnd:yyyyMMdd}.csv");
        }
    }
}