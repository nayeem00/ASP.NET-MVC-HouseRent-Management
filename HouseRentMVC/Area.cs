using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseRentMVC
{
    public class Area
    {
        public int Id { get; set; }
    [Required]
        public string AreaName { get; set; }
        [Required]
        public string SurroundingArea { get; set; }
    }
}