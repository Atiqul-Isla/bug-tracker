using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bug_tracker_web.Models;
using bug_tracker_web.ViewModel;
using System.Security.Claims;

namespace bug_tracker_web.Controllers
{
    public class BugController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BugController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bug
        public async Task<IActionResult> Index()
        {
            var bugs = await _context.Bugs.Include(b => b.Project).Include(b => b.BugUsers).ThenInclude(bu => bu.User).ToListAsync();

            return View(bugs);
        }

        // GET: Bug/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.Include(b => b.Comments).ThenInclude(c => c.User).FirstOrDefaultAsync(m => m.BugId == id);

            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bug/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName");
            var users = _context.Users.ToList();
            ViewData["BugUsers"] = new MultiSelectList(users, "Id", "UserName");
            return View();
        }

        // POST: Bug/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BugId,BugName,BugStatus,BugSeverity,BugCreatedBy,BugCreatedAt,BugDescription,ProjectID,SelectedUserIds")] Bug bug)
        {
            // Populate ProjectUsers using SelectedUserIds
            bug.BugUsers = bug.SelectedUserIds.Select(userId => new BugUser { UserId = userId }).ToList();

          
            _context.Add(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
       

            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", bug.ProjectID);
            var users = _context.Users.ToList();
            ViewData["BugUsers"] = new MultiSelectList(users, "Id", "UserName");

            return View(bug);
        }


        // GET: Bug/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bugs == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.Include(b => b.BugUsers).FirstOrDefaultAsync(b => b.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            var users = _context.Users.ToList();
            var selectedUserIds = bug.BugUsers.Select(pu => pu.UserId).ToList();

            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID", bug.ProjectID);
            ViewData["BugUsers"] = new MultiSelectList(users, "Id", "UserName", selectedUserIds);
            return View(bug);
        }

        // POST: Bug/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BugId,BugName,BugStatus,BugSeverity,BugCreatedBy,BugCreatedAt,BugDescription,ProjectID, SelectedUserIds")] Bug bug)
        {
            if (id != bug.BugId)
            {
                return NotFound();
            }

           
            try
            {
                // Remove duplicates from the selected user IDs
                var distinctUserIds = bug.SelectedUserIds.Distinct().ToList();

                // Delete existing project users associated with the project
                var existingBugUsers = _context.BugUsers.Where(pu => pu.BugId == bug.BugId);
                _context.BugUsers.RemoveRange(existingBugUsers);

                // Create a new instance of ProjectUser for each distinct user ID
                var bugUsers = distinctUserIds.Select(userId => new BugUser { BugId = bug.BugId, UserId = userId }).ToList();

                _context.Update(bug);
                _context.AddRange(bugUsers);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(bug.BugId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
            
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID", bug.ProjectID);
            return View(bug);
        }

        // GET: Bug/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bugs == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.Include(b => b.Project).FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bug/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bugs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bugs'  is null.");
            }
            var bug = await _context.Bugs.FindAsync(id);
            if (bug != null)
            {
                _context.Bugs.Remove(bug);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
          return (_context.Bugs?.Any(e => e.BugId == id)).GetValueOrDefault();
        }

        // GET: Display the "Add Comment" form
        [HttpGet]
        public IActionResult AddComment(int bugId)
        {
            var commentModel = new CommentViewModel
            {
                BugId = bugId
            };

            return View(commentModel);
        }

        // POST: Process the submitted comment
        [HttpPost]
        public IActionResult AddComment(CommentViewModel commentModel)
        {
            // Check if the comment model is valid
            
            // Retrieve the user ID of the currently logged-in user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Create a new Comment instance and populate its properties
            var comment = new Comment
            {
                CommentContent = commentModel.CommentContent,
                BugId = commentModel.BugId,
                UserId = userId
            };

            // Code to add the comment to the database
            _context.Comments.Add(comment); // Assuming _context is your database context
            _context.SaveChanges();

            // Redirect to the appropriate page
            return RedirectToAction("Details", "Bug", new { id = commentModel.BugId });
            

            // If the comment model is not valid, return to the same view with the validation errors
            return View(commentModel);
        }

        // GET: Bug/EditComment/5
        public async Task<IActionResult> EditComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            // Pass the comment to the view for editing
            return View(comment);
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }

        // POST: Bug/EditComment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComment(int id, [Bind("Id,CommentContent")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = comment.BugId });
            }

            // If the model state is invalid, return to the edit view with the comment
            return View(comment);
        }

        // GET: Bug/DeleteComment/5
        public async Task<IActionResult> DeleteComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var commentViewModel = new CommentViewModel
            {
                Id = comment.Id, // Set the desired Id value
                CommentContent = comment.CommentContent,
                BugId = comment.BugId,
                UserId = comment.UserId
            };

            // Pass the commentViewModel to the view for confirmation
            return View(commentViewModel);
        }

        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommentConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            var commentViewModel = new CommentViewModel
            {
                CommentContent = comment.CommentContent,
                BugId = comment.BugId,
                UserId = comment.UserId
            };

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Bug", new { id = commentViewModel.BugId });
        }

    }
}
