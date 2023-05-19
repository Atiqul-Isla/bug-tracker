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

        [Column(TypeName = "nvarchar(300)")]
        [Display(Name = "Description")]
        public string? ProjectDescription { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Version")]
        public string ProjectVersion { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime ProjectCreatedAt { get; set; } = DateTime.Now;


        //    [Column(TypeName = "nvarchar(450)")] // Change the type to match the ID type of DefaultUser
        //    public string ProjectCreatedByUserId { get; set; }  // FK to identify the user who created the project


        //    [ForeignKey(nameof(ProjectCreatedByUserId))]
        //    public DefaultUser ProjectCreatedByUser { get; set; } // Navigation property to access the user who created the project

        //    public ICollection<DefaultUser> AssignedUsers { get; set; } // Collection of users assigned to the project
        //}
    }
}
