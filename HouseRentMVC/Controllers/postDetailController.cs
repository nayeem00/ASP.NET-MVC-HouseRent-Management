using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class postDetailController : Controller
    {
        //
        // GET: /postDetail/
        [HttpGet]
        public ActionResult Index(int id)
        {
            HouseRentDBContext context = new HouseRentDBContext();
            var result = from e in context.Posts
                         join d in context.Areas
                         on e.AreaId equals d.Id
                         where e.Id == id
                         orderby e.Id descending
                         
                         
                         select new mypost
                         {
                             Id = e.Id,
                             HouseName = e.HouseName,

                             Image = e.Image,
                             Floor = e.Floor,
                             Bedroom = e.Bedroom,
                             Dining = e.Dining,
                             Drawing = e.Drawing,
                             Kitchen = e.Kitchen,
                             Category = e.Category,
                             Rent = e.Rent,
                             PhoneNumber = e.PhoneNumber,
                             AreaName = d.AreaName,
                             Address = e.Address,
                             Additinal = e.Additional,
                             RentStatus = e.RentStatus
                         };

            var result2 = from c in context.Comments
                         join u in context.Users
                         on c.UserId equals u.Id
                         join p in context.Posts
                         on c.PostId equals p.Id
                         where c.PostId == id


                         select new myComment
                         {
                             FullName = u.FullName,
                             Time = c.Time,
                             Comment = c.CommentText
                         };
            ViewBag.Post = result;
            ViewBag.PostId = id;
            ViewBag.Comments = result2;

            Post po = context.Posts.SingleOrDefault(x=>x.Id == id);
            var result3 = from e in context.Posts
                         join d in context.Areas
                         on e.AreaId equals d.Id
                         where e.AreaId == po.AreaId
                         where e.Rent < po.Rent
                         where e.Bedroom >= po.Bedroom
                         orderby e.Id descending


                         select new mypost
                         {
                             Id = e.Id,
                             HouseName = e.HouseName,

                             Image = e.Image,
                             Floor = e.Floor,
                             Bedroom = e.Bedroom,
                             Dining = e.Dining,
                             Drawing = e.Drawing,
                             Kitchen = e.Kitchen,
                             Category = e.Category,
                             Rent = e.Rent,
                             PhoneNumber = e.PhoneNumber,
                             AreaName = d.AreaName,
                             Address = e.Address,
                             Additinal = e.Additional,
                             RentStatus = e.RentStatus
                         };
            ViewBag.SuggestPost = result3;
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            if (Session["Login"] == null)
            {

                return RedirectToAction("Index", "Login");


            }
            HouseRentDBContext c = new HouseRentDBContext();
            Comment co = new Comment();
            User u = (User)Session["CurrentLoginUser"];
            co.UserId = u.Id;
            co.PostId =  Convert.ToInt32(f["postid"]);
            co.CommentText = f["comment"];
            co.Time = DateTime.Now;
            c.Comments.Add(co);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
