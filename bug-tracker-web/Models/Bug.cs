using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bug_tracker_web.Models
{
   
    public class Bug
    {
        [Key]
        public int BugId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Ticket Name")]
        public string BugName { get; set; }

		[Display(Name = "Status")]
		public string BugStatus { get; set; } // Change the type to string

		[Display(Name = "Severity")]
		public string BugSeverity { get; set; } // Change the type to string

		[Display(Name = "Date")]
        public DateTime BugCreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(2000)")]
        [Display(Name = "Description")]
        public string BugDescription { get; set; }

        //Relationships
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        [Display(Name = "Users")]
        public List<BugUser> BugUsers { get; set; }

        [NotMapped]
        public List<string> SelectedUserIds { get; set; } = new List<string>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
