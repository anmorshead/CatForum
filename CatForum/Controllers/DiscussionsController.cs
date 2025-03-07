using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatForum.Data;
using CatForum.Models;
using NuGet.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CatForum.Controllers
{
    //only logged in users have access
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly CatForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        //constructor
        public DiscussionsController(CatForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Discussions - My Discussions: get all discusions by user id
        public async Task<IActionResult> Index()
        {

            var userId = _userManager.GetUserId(User);

            var discussions = await _context.Discussion
                .Where(m => m.ApplicationUserId == userId) //filter by userId
                .ToListAsync();
              
            return View(discussions);
           
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionId,Title,Content,CreateDate,ImageFile")] Discussion discussion)
        {
            // Set the userId of the person logged in
            discussion.ApplicationUserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                // Only generate a file name if an image is uploaded
                if (discussion.ImageFile != null)
                {
                    discussion.ImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile.FileName);
                }

                // Save in db
                _context.Add(discussion);
                await _context.SaveChangesAsync();

                // Save uploaded file after discussion is saved in db.
                if (discussion.ImageFile != null)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", discussion.ImageFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(fileStream);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }


        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get the logged in user ID
            var userId = _userManager.GetUserId(User);


            
            var discussion = await _context.Discussion
                .Include(m => m.Comments) //Include the comments list
                .Where(m => m.ApplicationUserId == userId) //filter by userId
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,CreateDate,ApplicationUserId")] Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get the logged in user ID
            var userId = _userManager.GetUserId(User);

            var discussion = await _context.Discussion
                .Where(m => m.ApplicationUserId == userId) //filter by user id
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            // get the logged in user ID
            var userId = _userManager.GetUserId(User);

            var discussion = await _context.Discussion
                .Where(m => m.ApplicationUserId == userId) // filter by user Id
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }
            else
            {
                _context.Discussion.Remove(discussion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }
    }
}
