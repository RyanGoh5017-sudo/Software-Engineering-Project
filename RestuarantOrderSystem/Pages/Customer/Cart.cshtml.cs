using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using RestuarantOrderSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Customer
{
    public class CartModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CartModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CartItemDto> CartItems { get; set; } = new();
        public decimal TotalAmount { get; set; }

        [BindProperty]
        public string PaymentMethod { get; set; } = "Credit Card";

        public void OnGet()
        {
            CartItems = HttpContext.Session.GetCart();

            // Safeguard against negative prices or quantities in session
            foreach (var item in CartItems)
            {
                item.Price = Math.Max(0, item.Price);
                item.Quantity = Math.Max(1, item.Quantity);
            }

            // Calculate total with negative-value protection
            TotalAmount = Math.Max(0, CartItems.Sum(c => c.Price * c.Quantity));
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            CartItems = HttpContext.Session.GetCart();
            if (!CartItems.Any()) return RedirectToPage("/Customer/Menu");

            // Safeguard prices and quantities before checkout processing
            foreach (var item in CartItems)
            {
                item.Price = Math.Max(0, item.Price);
                item.Quantity = Math.Max(1, item.Quantity);
            }

            // Check if user is logged in (Nullable for Guest Checkout support)
            int? userId = null;
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim, out int parsedUserId))
            {
                userId = parsedUserId;
            }

            decimal finalTotal = Math.Max(0, CartItems.Sum(c => c.Price * c.Quantity));

            var order = new Order
            {
                UserId = userId, // Attached if logged in, null if checking out as guest
                TotalAmount = finalTotal,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.UtcNow
            };

            foreach (var item in CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                });
            }

            var payment = new Payment
            {
                Order = order,
                Amount = order.TotalAmount,
                PaymentMethod = PaymentMethod,
                Status = PaymentStatus.Paid,
                PaymentDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            HttpContext.Session.ClearCart();

            // Redirect registered members to order history; guests directly to menu or confirmation
            if (userId.HasValue)
            {
                return RedirectToPage("/Customer/Orders");
            }

            return RedirectToPage("/Customer/Menu");
        }
    }
}