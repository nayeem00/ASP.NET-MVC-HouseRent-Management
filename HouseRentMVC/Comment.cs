using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HouseRentMVC
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }

       
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