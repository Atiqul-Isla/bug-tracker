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
        public string BugName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BugStatus { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BugSeverity { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BugCreatedBy { get; set; }

        public DateTime BugCreatedAt { get; set; } = DateTime.Now;

        //AssignedTo

        [Column(TypeName = "nvarchar(500)")]
        public string BugDescription { get; set; }

        public int? ProjectID { get; set; }
        public Project Project { get; set; }

    }
}
