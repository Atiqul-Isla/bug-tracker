﻿using System.ComponentModel.DataAnnotations;

namespace bug_tracker_web.ViewModel
{
    public class AddRoleViewModel
    {

        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
