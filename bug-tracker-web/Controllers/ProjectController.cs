using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bug_tracker_web.Models;
using bug_tracker_web.ViewModel;

namespace bug_tracker_web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.Include(b => b.Bugs).Include(p => p.ProjectUsers).ThenInclude(pu => pu.User).ToListAsync();

            return View(projects);
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectID == id);
            var bugs = await _context.Bugs.Where(b => b.ProjectID == id).ToListAsync();

            var viewModel = new ProjectDetailsViewModel
            {
                Project = project,
                Bugs = bugs
            };


            if (project == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            var users = _context.Users.ToList();
            ViewData["ProjectUsers"] = new MultiSelectList(users, "Id", "UserName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,ProjectName,ProjectType,ProjectDescription,ProjectVersion,ProjectCreatedAt,SelectedUserIds")] Project project)
        {
            if (ModelState.IsValid)
            {
                // Remove duplicates from the selected user IDs
                var distinctUserIds = project.SelectedUserIds.Distinct().ToList();

                // Add the project to the context
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                // Create a new instance of ProjectUser for each distinct user ID
                var projectUsers = distinctUserIds.Select(userId => new ProjectUser { ProjectId = project.ProjectID, UserId = userId }).ToList();

                // Add the project users to the context and save changes
                _context.ProjectUsers.AddRange(projectUsers);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var users = _context.Users.ToList();
            ViewData["ProjectUsers"] = new SelectList(users, "Id", "UserName");

            return View(project);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.Include(p => p.ProjectUsers).FirstOrDefaultAsync(p => p.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }

            var users = _context.Users.ToList();
            var selectedUserIds = project.ProjectUsers.Select(pu => pu.UserId).ToList();
            ViewData["ProjectUsers"] = new MultiSelectList(users, "Id", "UserName", selectedUserIds);

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,ProjectName,ProjectType,ProjectDescription,ProjectVersion,ProjectCreatedAt,SelectedUserIds")] Project project)
        {
            if (id != project.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Remove duplicates from the selected user IDs
                    var distinctUserIds = project.SelectedUserIds.Distinct().ToList();

                    // Delete existing project users associated with the project
                    var existingProjectUsers = _context.ProjectUsers.Where(pu => pu.ProjectId == project.ProjectID);
                    _context.ProjectUsers.RemoveRange(existingProjectUsers);

                    // Create a new instance of ProjectUser for each distinct user ID
                    var projectUsers = distinctUserIds.Select(userId => new ProjectUser { ProjectId = project.ProjectID, UserId = userId }).ToList();

                    // Update the project and add the new project users
                    _context.Update(project);
                    _context.AddRange(projectUsers);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectID))
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

            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return (_context.Projects?.Any(e => e.ProjectID == id)).GetValueOrDefault();
        }
    }
}
