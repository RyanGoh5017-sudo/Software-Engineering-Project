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
    public class OrdersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrdersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.MenuItem)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.Include(o => o.Table).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = newStatus;

                // Automatically free table when order is marked Completed or Cancelled
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