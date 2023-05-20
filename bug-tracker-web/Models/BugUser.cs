using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bug_tracker_web.Models
{
    public class BugUser
    {
        [Key]
        [Column(Order = 1)]
        public int BugId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }
       
        public DefaultUser User { get; set; }
        public Bug Bug { get; set; }
    }
}
