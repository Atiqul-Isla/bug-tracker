using System.ComponentModel.DataAnnotations;

namespace bug_tracker_web.ViewModel
{
    public class EditRoleViewModel
    {

        public string Id { get; set;}

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set;}
        public List<String> Users { get; set; } = new();
    }
}
