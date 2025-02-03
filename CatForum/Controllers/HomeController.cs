using CatForum.Data;
using CatForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CatForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatForumContext _context;

        public HomeController(CatForumContext context)
        {
            _context = context;
        }

        // Display all discussions on the homepage
        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion.OrderByDescending(m => m.CreateDate).ToListAsync();
            return View(discussions);
        }

        // Show details of a specific discussion
        public async Task<IActionResult> DiscussionDetails(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments) // Load comments if needed
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound(); 
            }

            return View(discussion);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
