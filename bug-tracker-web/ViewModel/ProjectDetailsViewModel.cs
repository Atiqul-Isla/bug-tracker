using bug_tracker_web.Models;

namespace bug_tracker_web.ViewModel
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; }
        public List<Bug> Bugs { get; set; }
    }
}
