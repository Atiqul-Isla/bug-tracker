using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bug_tracker_web.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Name")]
        public string ProjectName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Type")]
        public string ProjectType { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        [Display(Name = "Description")]
        public string? ProjectDescription { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Version")]
        public string ProjectVersion { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime ProjectCreatedAt { get; set; } = DateTime.Now;

        public ICollection<Bug> Bugs { get; set; } = new List<Bug>();

        //Many-Many
        public List<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();

        [NotMapped]
        public List<string> SelectedUserIds { get; set; } = new List<string>();

    }
}
