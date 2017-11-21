using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HouseRentMVC
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string HouseName { get; set; }
        [Required]
        public string Address { get; set; }
        [RegularExpression(@"^[0-9]{11}$",
        ErrorMessage = "Invalid Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
        public int Floor { get; set; }
        public int Bedroom { get; set; }
        public bool Dining { get; set; }
        public bool Drawing { get; set; }
        public bool Kitchen { get; set; }
        public string Additional { get; set; }
        public string Category { get; set; }
        public int Approval { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int CreatedBy { get; set; }
        public int AreaId { get; set; }
        public int HireStatus { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Required]
        public int Rent { get; set; }
        public string RentStatus { get; set; }
        public string Image { get; set; }
    }
}