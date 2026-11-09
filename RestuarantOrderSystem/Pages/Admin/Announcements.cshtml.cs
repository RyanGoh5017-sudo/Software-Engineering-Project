using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Admin
{
    public class AnnouncementsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SystemAnnouncement> Announcements { get; set; } = new();

        [BindProperty]
        public SystemAnnouncement NewAnnouncement { get; set; } = new();

        public async Task OnGetAsync()
        {
            Announcements = await _context.SystemAnnouncements.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.SystemAnnouncements.Add(NewAnnouncement);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}