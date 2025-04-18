using CatForum.Data;
using CatForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
            var discussions = await _context.Discussion
                .OrderByDescending(m => m.CreateDate)
                .Include(d => d.ApplicationUser)
                .Include(d => d.Comments).ToListAsync();
            return View(discussions);
        }

        // Show details of a specific discussion
        public async Task<IActionResult> DiscussionDetails(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments) 
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GetDiscussion details
        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.ApplicationUser)
                .Include(d => d.Comments)
                .ThenInclude(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            // Sort comments in descending order by CreateDate
            discussion.Comments = discussion.Comments.OrderByDescending(c => c.CreateDate).ToList();

            return View(discussion);
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Discussions) // Include user's discussions
                .ThenInclude(m => m.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }



        public IActionResult Privacy()
        {
            return View();
        }
    }
}
