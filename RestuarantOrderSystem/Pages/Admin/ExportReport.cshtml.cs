using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Services;
using System;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class ExportReportModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PdfReportService _pdfService;

        public ExportReportModel(ApplicationDbContext context, PdfReportService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        public async Task<IActionResult> OnGetAsync(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.UtcNow.AddDays(-30);
            var end = endDate ?? DateTime.UtcNow;

            var orders = await _context.Orders
                .Where(o => o.OrderDate >= start && o.OrderDate <= end && o.Status == Models.OrderStatus.Completed)
                .ToListAsync();

            byte[] pdf = _pdfService.GenerateSalesReport(orders, start, end);
            return File(pdf, "application/pdf", $"Sales_Report_{start:yyyyMMdd}_to_{end:yyyyMMdd}.pdf");
        }
    }
}