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
            var discussions = await _context.Discussion.OrderByDescending(m => m.CreateDate).Include(d => d.Comments).ToListAsync();
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
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            // Sort comments in descending order by CreateDate
            discussion.Comments = discussion.Comments.OrderByDescending(c => c.CreateDate).ToList();

            return View(discussion);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
