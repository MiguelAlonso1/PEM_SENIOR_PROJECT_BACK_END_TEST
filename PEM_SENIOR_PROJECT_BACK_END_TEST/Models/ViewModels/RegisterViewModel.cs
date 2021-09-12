using PEM_SENIOR_PROJECT_BACK_END_TEST.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //{0} is Password
        //{1} is 100
        //{2} is 6
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage ="The password and the confirmation do not match ")]
        public string ConfirmPassword { get; set; }


        //[Range(1, int.MaxValue, ErrorMessage = "Please select a Role Type!")]
        //public int RoleNameId { get; set; }

        [Required]
        [DisplayName("Role Name")]
       // [ForeignKey("RoleNameId")]
        public string RoleName { get; set; }
    }
}