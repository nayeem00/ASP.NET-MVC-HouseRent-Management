using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HouseRentMVC
{
    public class HouseRentDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
    }
}