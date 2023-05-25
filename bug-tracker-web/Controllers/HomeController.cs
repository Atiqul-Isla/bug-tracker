using bug_tracker_web.Models;
using bug_tracker_web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace bug_tracker_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the data required for the dashboard
            var totalProjects = _context.Projects.Count();
            var totalBugs = _context.Bugs.Count();
            var totalComments = _context.Comments.Count();
            var bugsOverTime = _context.Bugs.OrderBy(b => b.BugCreatedAt).Select(b => b.BugCreatedAt).ToList();
            var projectsWithMostBugs = _context.Projects.OrderByDescending(p => p.Bugs.Count).FirstOrDefault();
            var personnel = _context.Users.ToList();

            // Create the view model
            var viewModel = new HomeViewModel
            {
                TotalProjects = totalProjects,
                TotalBugs = totalBugs,
                TotalComments = totalComments,
                BugsOverTime = bugsOverTime,
                ProjectsWithMostBugs = projectsWithMostBugs,
                Personnel = personnel
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
