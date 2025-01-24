using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatForum.Data;
using CatForum.Models;

namespace CatForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CatForumContext _context;

        public CommentsController(CatForumContext context)
        {
            _context = context;
        }

        // GET: Comments
        //public async Task<IActionResult> Index()
        // {
        //   var catForumContext = _context.Comment.Include(c => c.Discussion);
        //   return View(await catForumContext.ToListAsync());
        // }

        // GET: Comments/Create
        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id == null)
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
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreateDate,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                
                // Redirect to the Details page of the associated discussion
                return RedirectToAction("Details", "Discussions", new { id = comment.DiscussionId });
            }
            ViewData["DiscussionId"] = new SelectList(_context.Set<Discussion>(), "DiscussionId", "DiscussionId", comment.DiscussionId);
            return View(comment);
        }


        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}
