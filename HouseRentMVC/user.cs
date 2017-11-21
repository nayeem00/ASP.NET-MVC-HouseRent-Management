using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HouseRentMVC
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password", 
        ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{11}$",
        ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string FullName { get; set; }
        [DefaultValue("user")]
        public string UserStatus { get; set; }
    }
}