using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HouseRentMVC
{
    public class DBSeeder : DropCreateDatabaseIfModelChanges<HouseRentDBContext>
    {
        protected override void Seed(HouseRentDBContext context)
        {
            
            Area a = new Area()
            {
                AreaName = "Mirpur-10",
                SurroundingArea = "Mirpur-2,Mipur-13,Mirpur-11,Kazipara,Purobi",
               
            };
            context.Areas.Add(a);
            User u = new User()
            {
                Username = "nayeem",
                Password = "1234",
                ConfirmPassword = "1234",
                FullName = "Nayeem Hossain",
                PhoneNumber = "01743349711"
            
             };
            context.Users.Add(u);


            context.SaveChanges();
            base.Seed(context);
        }
    }
}