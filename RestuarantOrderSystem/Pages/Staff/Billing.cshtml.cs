using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Staff
{
    public class BillingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BillingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Payment> Payments { get; set; } = new();

        public async Task OnGetAsync()
        {
            Payments = await _context.Payments.Include(p => p.Order).ToListAsync();
        }
    }
}