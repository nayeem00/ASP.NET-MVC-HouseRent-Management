using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseRentMVC.Controllers
{
    public class mypost
    {
        public int Id { get; set; }
        public String HouseName { get; set; }

        public String Image { get; set; }
        public int Floor { get; set; }
        public int  Bedroom { get; set; }
        public bool  Dining { get; set; }
        public bool  Drawing { get; set; }
        public bool  Kitchen { get; set; }
        public String Category { get; set; }
        public int Rent { get; set; }
        public String PhoneNumber { get; set; }
        public String AreaName { get; set; }
        public String Address { get; set; }
        public String Additinal { get; set; }
        public String RentStatus { get; set; }
        public int returnboolstat(bool x)
        {
            if(x)
            {
                return 1;
            }
            else{
                return 0;
            }
        }
    }
}
