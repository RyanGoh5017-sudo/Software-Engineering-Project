using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class RestaurantSettingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RestaurantSettingsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RestaurantInfo Info { get; set; } = new();

        public async Task OnGetAsync()
        {
            Info = await _context.RestaurantInfos.FirstOrDefaultAsync() ?? new RestaurantInfo();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.RestaurantInfos.Update(Info);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}