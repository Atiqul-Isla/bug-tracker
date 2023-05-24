using bug_tracker_web.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace bug_tracker_web.ViewModel
{
    public class ListAllRolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public List<DefaultUser> Users { get; set; }
        public UserManager<DefaultUser> UserManager { get; set; } // Add the UserManager property
    }
}
