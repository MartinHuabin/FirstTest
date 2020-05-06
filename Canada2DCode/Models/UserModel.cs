using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Canada2DCode.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        [Display(Name = "User name")]
        public override string UserName { get; set; }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public override string Email { get; set; }
        public string Url { get; set; }
        public bool Conditions_BT { get; set; }
    }
}