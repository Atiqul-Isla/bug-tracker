using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace bug_tracker_web.Models
{
    public class DefaultUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public string WorkType { get; set; }
        
        //[PersonalData]
        //public IEnumerable<SelectListItem> WorkTypeOptions { get; set; }

        [PersonalData]
        public string? Address { get; set; }

        [PersonalData]
        public string? ZipCode { get; set; }

        [PersonalData]
        public string City { get; set; }

        [PersonalData]
        public Boolean IsOnline { get; set; }

        //Many-Many
        [InverseProperty("User")]
        public List<ProjectUser> ProjectUsers { get; set; }

        [InverseProperty("User")]
        public List<BugUser> BugUsers { get; set; }
    }
}
