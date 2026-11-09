using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Services;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Customer
{
    public class MenuModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MenuModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<MenuItem> MenuItems { get; set; } = new();
        public int CartCount { get; set; }

        public async Task OnGetAsync()
        {
            MenuItems = await _context.MenuItems.Where(m => m.IsAvailable).ToListAsync();
            CartCount = HttpContext.Session.GetCart().Sum(c => c.Quantity);
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int itemId)
        {
            var item = await _context.MenuItems.FindAsync(itemId);
            if (item == null || !item.IsAvailable) return RedirectToPage();

            // Determine effective price: use discount price if promotional, otherwise regular price
            decimal effectivePrice = (item.IsPromotional && item.DiscountPrice > 0) ? item.DiscountPrice : item.Price;

            // Protect against any negative price values
            effectivePrice = Math.Max(0, effectivePrice);

            var cart = HttpContext.Session.GetCart();
            var existing = cart.FirstOrDefault(c => c.MenuItemId == itemId);
            if (existing != null)
            {
                existing.Quantity++;
                // Optionally update price to current promotional price if needed
                existing.Price = effectivePrice;
            }
            else
            {
                cart.Add(new CartItemDto
                {
                    MenuItemId = item.MenuItemId,
                    Name = item.Name,
                    Price = effectivePrice,
                    Quantity = 1
                });
            }

            HttpContext.Session.SaveCart(cart);
            return RedirectToPage();
        }
    }
}