using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HouseRentMVC
{
    public class SearchHistory
    {
        public int Id { get; set; }
        public string KeyWord { get; set; }
        public string Ip { get; set; }
        public string RoomFilter { get; set; }
        
        public string PriceFilter { get; set; }
        public string CategoryFilter { get; set; }

        public DateTime Time
        {
            get
            {
                return this.time.HasValue
                   ? this.time.Value
                   : DateTime.Now;
            }

            set { this.time = value; }
        }

        private DateTime? time = null;
    }
}