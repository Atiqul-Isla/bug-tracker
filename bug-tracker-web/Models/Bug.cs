using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bug_tracker_web.Models
{
    public enum BugSeverity
    {
        Low,
        Moderate,
        Important,
        Critical
    }
    public class Bug
    {
        [Key]
        public int BugId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BugName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BugStatus { get; set; }

        public BugSeverity BugSeverity { get; set; }

        public DateTime BugCreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(500)")]
        public string BugDescription { get; set; }

        //Relationships
        public int? ProjectID { get; set; }
        public Project Project { get; set; }

        public List<BugUser> BugUsers { get; set; }

        [NotMapped]
        public List<string> SelectedUserIds { get; set; } = new List<string>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
