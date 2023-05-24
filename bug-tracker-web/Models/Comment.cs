using bug_tracker_web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    [Key]
    public int Id { get; set; }

    public string CommentContent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Relationships
    public int BugId { get; set; }
    public Bug Bug { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public DefaultUser User { get; set; }
}
