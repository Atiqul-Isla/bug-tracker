using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bug_tracker_web.Models
{
    public class ProjectUser
    {

        [Key]
        [Column(Order =1)]
        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }

        public Project Project { get; set; }
        public DefaultUser User { get; set; }

    }
}
