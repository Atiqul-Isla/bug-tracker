using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bug_tracker_web.Models;

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
            var applicationDbContext = _context.Bugs.Include(b => b.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bug/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bugs == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bug/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BugId,BugName,BugStatus,BugSeverity,BugCreatedBy,BugCreatedAt,BugDescription,ProjectID")] Bug bug)
        {
            
            _context.Add(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            

        }

        // GET: Bug/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bugs == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID", bug.ProjectID);
            return View(bug);
        }

        // POST: Bug/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BugId,BugName,BugStatus,BugSeverity,BugCreatedBy,BugCreatedAt,BugDescription,ProjectID")] Bug bug)
        {
            if (id != bug.BugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
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
            }
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

            var bug = await _context.Bugs
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.BugId == id);
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
    }
}
