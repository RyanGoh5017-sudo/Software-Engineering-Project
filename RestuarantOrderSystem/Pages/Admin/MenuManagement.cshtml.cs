using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class MenuManagementModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public MenuManagementModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public List<MenuItem> MenuItems { get; set; } = new();
        public List<MenuCategory> Categories { get; set; } = new();

        [BindProperty]
        public MenuItem MenuItem { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task OnGetAsync()
        {
            MenuItems = await _context.MenuItems.Include(m => m.Category).ToListAsync();
            Categories = await _context.MenuCategories.ToListAsync();
        }

        // Unified Handler for Create and Edit
        public async Task<IActionResult> OnPostSaveAsync()
        {
            // Ensure Description is never null to prevent database SqlException
            MenuItem.Description ??= string.Empty;

            // Handle Image Upload if a file was selected
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "menu");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                MenuItem.ImageUrl = "/images/menu/" + uniqueFileName;
            }

            if (MenuItem.MenuItemId == 0)
            {
                // Insert New Item
                _context.MenuItems.Add(MenuItem);
            }
            else
            {
                // Update Existing Item
                var existingItem = await _context.MenuItems.FindAsync(MenuItem.MenuItemId);
                if (existingItem != null)
                {
                    existingItem.Name = MenuItem.Name;
                    existingItem.Description = MenuItem.Description;
                    existingItem.Price = MenuItem.Price;
                    existingItem.CategoryId = MenuItem.CategoryId;
                    existingItem.IsAvailable = MenuItem.IsAvailable;
                    existingItem.IsPromotional = MenuItem.IsPromotional;
                    existingItem.DiscountPrice = MenuItem.DiscountPrice;

                    // Only update ImageUrl if a new file was uploaded
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        existingItem.ImageUrl = MenuItem.ImageUrl;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        // Delete Handler
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}