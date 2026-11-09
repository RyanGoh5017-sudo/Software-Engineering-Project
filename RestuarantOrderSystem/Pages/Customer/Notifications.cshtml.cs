using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestuarantOrderSystem.Pages.Customer
{
    public class NotificationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NotificationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SystemAnnouncement> Announcements { get; set; } = new();

        public async Task OnGetAsync()
        {
            Announcements = await _context.SystemAnnouncements.ToListAsync();
        }
    }
}