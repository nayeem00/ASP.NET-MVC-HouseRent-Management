using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class AdminPanelController : Controller
    {
        
            
        

        //
        // GET: /AdminPanel/

        public ActionResult Index()
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
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


            var Approved = from e in context.Posts
                             join d in context.Areas
                             on e.AreaId equals d.Id
                             where e.Approval == 1
                             orderby e.Id descending
                             select new mypost
                             {
                                 Id = e.Id,
                                 HouseName = e.HouseName,

                                 Image = e.Image,

                                 AreaName = d.AreaName
                             };
            ViewBag.Approved = Approved;


            var Pending = from e in context.Posts
                           join d in context.Areas
                           on e.AreaId equals d.Id
                           where e.Approval == 0
                           orderby e.Id descending
                           select new mypost
                           {
                               Id = e.Id,
                               HouseName = e.HouseName,

                               Image = e.Image,

                               AreaName = d.AreaName
                           };
            ViewBag.Pending = Pending;


            ViewBag.area = context.Areas;

            ViewBag.User = from e in context.Users
                           
                           where e.UserStatus == "user"
                           select e ;

            ViewBag.Admin = from e in context.Users

                           where e.UserStatus == "admin"
                           select e;

            return View();
        }
        public ActionResult deletePost(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
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
            p = c.Posts.SingleOrDefault(x => x.Id == id);
            c.Posts.Remove(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult report()
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            HouseRentDBContext context = new HouseRentDBContext();
            var MaxArea = from p in context.Posts
                           join a in context.Areas
                           on p.AreaId equals a.Id
                           where p.HireStatus == 0
                           where p.Approval == 1
                           group a by p.AreaId into Group
                           orderby Group.Count() descending
                           select new 
                           {
                              area = Group.Key,
                              Count = Group.Count()
                           };
            
            int count = 0;
            int key = 0;
            
            foreach (var item in MaxArea)
            {
                if (count!=0)
                {
                    break;
                }
                count++;
                key = item.area;
            }

            ViewBag.AreaName = context.Areas.SingleOrDefault(X=> X.Id == key).AreaName ;


            var ActiveUser = from p in context.Posts
                          join a in context.Users
                          on p.CreatedBy equals a.Id
                          
                          group a by p.CreatedBy into Group
                          orderby Group.Count() descending
                          select new
                          {
                              area = Group.Key,
                              Count = Group.Count()
                          };
           
            int count2 = 0;
            int key2 = 0;

            foreach (var item in ActiveUser)
            {
                if (count2 != 0)
                {
                    break;
                }
                count2++;
                key2 = item.area;
            }

            ViewBag.UserName = context.Users.SingleOrDefault(X => X.Id == key2).Username;

            var SKeyWord = from p in context.SearchHistories
                             
                            
                             group p by p.KeyWord into Group
                             orderby Group.Count() descending
                             select new
                             {
                                 area = Group.Key,
                                 Count = Group.Count()
                             };

            int count3 = 0;
            string key3 = null;

            foreach (var item in SKeyWord)
            {
                if (count3 != 0)
                {
                    break;
                }
                count3++;
                key3 = item.area;
            }

            ViewBag.KeyWord = key3;

            var ip = from p in context.SearchHistories


                           group p by p.Ip into Group
                           orderby Group.Count() descending
                           select new
                           {
                               area = Group.Key,
                               Count = Group.Count()
                           };

            int count4 = 0;
            string key4 = null;

            foreach (var item in ip)
            {
                if (count4 != 0)
                {
                    break;
                }
                count4++;
                key4 = item.area;
            }

            ViewBag.Ip = key4;
            


            return View();
        }
        public ActionResult approvePost(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            Session["message"] = "Post Approved";

            HouseRentDBContext c = new HouseRentDBContext();
            Post p = new Post();
            p = c.Posts.SingleOrDefault(x => x.Id == id);
            p.Approval = 1;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult addarea()
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult addarea(Area a)
        {
            Session["message"] = "New Area Added";

            HouseRentDBContext c = new HouseRentDBContext();
           
            
            c.Areas.Add(a);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult deleteArea(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            Session["message"] = "Area Deleted";

            HouseRentDBContext c = new HouseRentDBContext();
            Area a = new Area();
            a = c.Areas.SingleOrDefault(x => x.Id == id);
            c.Areas.Remove(a);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult deleteUser(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            Session["message"] = "User Deleted";

            HouseRentDBContext c = new HouseRentDBContext();
            User p = new User();
            p = c.Users.SingleOrDefault(x => x.Id == id);
            c.Users.Remove(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MakeAdmin(int id)
        {
            if (Session["Login"] != null)
            {
                if ((string)Session["user"] != "admin")
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            Session["message"] = "New Admin Added";

            HouseRentDBContext c = new HouseRentDBContext();
            User p = new User();
            p = c.Users.SingleOrDefault(x => x.Id == id);
            p.UserStatus = "admin";
            c.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
