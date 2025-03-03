using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatForum.Data;
using CatForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace CatForum.Controllers
{
    //only logged in users have access
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly CatForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(CatForumContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //Deleted the following actions:
        // GET: Comments
        // GET: Comments/Details/5
        // POST: Comments/Edit/5
        // POST: Comments/Delete/5

        // GET: Comments/Create
        // [HttpGet] needed?
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get the logged in user ID
            var userId = _userManager.GetUserId(User);

            var comment = await _context.Discussion
                .Where(m => m.ApplicationUserId == userId) // filter by user Id
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (comment == null)
            {
                return NotFound();
            }

            ViewData["DiscussionId"] = id;

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreateDate,DiscussionId,ApplicationUserId")] Comment comment)
        {
            //set the userId of the person logged in
            comment.ApplicationUserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                
                // Redirect to the GetDiscussion page of the associated discussion
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }
            ViewData["DiscussionId"] = comment.DiscussionId;
            return View(comment);
        }


        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}
