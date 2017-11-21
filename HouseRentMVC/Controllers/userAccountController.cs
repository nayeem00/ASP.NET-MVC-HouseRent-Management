using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class userAccountController : Controller
    {
        //
        // GET: /userAccount/

        public ActionResult Index()
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "user")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            string temp = (string)Session["message"];
            Session["message"] = "";
            ViewBag.message = temp;
            HouseRentDBContext context = new HouseRentDBContext();
            User u = (User)Session["CurrentLoginUser"];
            var ActivePost = from e in context.Posts
                         join d in context.Areas
                         on e.AreaId equals d.Id
                         where e.CreatedBy == u.Id
                         where e.HireStatus == 0
                         orderby e.Id descending
                         select new mypost
                         {
                             Id = e.Id,
                             HouseName = e.HouseName,

                             Image = e.Image,
                             
                             AreaName = d.AreaName
                         };
            ViewBag.ActivePost = ActivePost;

            var InActivePost = from e in context.Posts
                             join d in context.Areas
                             on e.AreaId equals d.Id
                               where e.CreatedBy == u.Id 
                               where e.HireStatus == 1
                               
                             orderby e.Id descending
                             select new mypost
                             {
                                 Id = e.Id,
                                 HouseName = e.HouseName,

                                 Image = e.Image,

                                 AreaName = d.AreaName
                             };
            ViewBag.InActivePost = InActivePost;
            return View();
        }
        public ActionResult deletePost(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "user")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            Session["message"] = "Post Deleted";

            HouseRentDBContext c = new HouseRentDBContext();
            Post p = new Post();
            p = c.Posts.SingleOrDefault(x=>x.Id==id);
            c.Posts.Remove(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DisablePost(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "user")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            Session["message"] = "Post DeActived";
            HouseRentDBContext c = new HouseRentDBContext();
            Post p = new Post();
            p = c.Posts.SingleOrDefault(x => x.Id == id);
            p.HireStatus = 1;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ActivatePost(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "user")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            Session["message"] = "Post Actived";
            HouseRentDBContext c = new HouseRentDBContext();
            Post p = new Post();
            p = c.Posts.SingleOrDefault(x => x.Id == id);
            p.HireStatus = 0;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
