using bug_tracker_web.Models;
using System;
using System.Collections.Generic;

namespace bug_tracker_web.ViewModel
{
    public class HomeViewModel
    {
        public List<Project> Projects { get; set; }
        public List<Bug> Bugs { get; set; }
        public List<Comment> Comments { get; set; }
        public List<DefaultUser> Users { get; set; }
        public List<BugUser> BugUsers { get; set; }
        public List<ProjectUser> ProjectUsers { get; set; }

        // Additional properties for the dashboard
        public int TotalProjects { get; set; }
        public int TotalBugs { get; set; }
        public int TotalComments { get; set; }
        public List<DateTime> BugsOverTime { get; set; }
        public Project ProjectsWithMostBugs { get; set; }
        public List<DefaultUser> Personnel { get; set; }
    }
}
