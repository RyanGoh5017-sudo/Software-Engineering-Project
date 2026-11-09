using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace RestuarantOrderSystem.Services
{
    public class CartItemDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public static class CartSessionExtensions
    {
        private const string CartKey = "CustomerCart";

        public static List<CartItemDto> GetCart(this ISession session)
        {
            var data = session.GetString(CartKey);
            return data == null ? new List<CartItemDto>() : JsonSerializer.Deserialize<List<CartItemDto>>(data) ?? new List<CartItemDto>();
        }

        public static void SaveCart(this ISession session, List<CartItemDto> cart)
        {
            session.SetString(CartKey, JsonSerializer.Serialize(cart));
        }

        public static void ClearCart(this ISession session)
        {
            session.Remove(CartKey);
        }
    }
}