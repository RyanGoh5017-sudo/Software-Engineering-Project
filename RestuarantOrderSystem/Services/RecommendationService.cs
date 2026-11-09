using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Services
{
    public class RecommendationService
    {
        private readonly ApplicationDbContext _context;

        public RecommendationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetRecommendationsForUserAsync(int userId)
        {
            var orderedItemIds = await _context.OrderItems
                .Where(oi => oi.Order!.UserId == userId)
                .Select(oi => oi.MenuItemId)
                .Distinct()
                .ToListAsync();

            if (orderedItemIds.Any())
            {
                var favoriteCategoryId = await _context.OrderItems
                    .Where(oi => oi.Order!.UserId == userId)
                    .GroupBy(oi => oi.MenuItem!.CategoryId)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefaultAsync();

                var personalized = await _context.MenuItems
                    .Where(m => m.CategoryId == favoriteCategoryId && m.IsAvailable)
                    .Take(3)
                    .ToListAsync();

                if (personalized.Any()) return personalized;
            }

            return await _context.OrderItems
                .GroupBy(oi => oi.MenuItem)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Select(g => g.Key!)
                .Where(m => m.IsAvailable)
                .Take(3)
                .ToListAsync();
        }
    }
}