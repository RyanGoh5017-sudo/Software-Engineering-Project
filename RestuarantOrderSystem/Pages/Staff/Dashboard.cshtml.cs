using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Staff
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> ActiveOrders { get; set; } = new();

        public async Task OnGetAsync()
        {
            ActiveOrders = await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(i => i.MenuItem)
                .Include(o => o.Table)
                .Where(o => o.Status != OrderStatus.Completed && o.Status != OrderStatus.Cancelled)
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.Include(o => o.Table).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = newStatus;

                if ((newStatus == OrderStatus.Completed || newStatus == OrderStatus.Cancelled) && order.Table != null)
                {
                    order.Table.Status = TableStatus.Available;
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}