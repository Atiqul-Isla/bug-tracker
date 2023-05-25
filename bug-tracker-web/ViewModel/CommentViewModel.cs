using bug_tracker_web.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bug_tracker_web.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string CommentContent { get; set; }
        public int BugId { get; set; }
        public string UserId { get; set; }
    }

}