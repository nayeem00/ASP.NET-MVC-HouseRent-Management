using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.messege = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            HouseRentDBContext context = new HouseRentDBContext();
            if (ModelState.IsValid)
            {
                User temp = context.Users.SingleOrDefault(x=> x.Username==u.Username);
                if(temp != null)
                {
                    ViewBag.messege = "UserName Already exist";
                    return View(u);
                }
                u.UserStatus = "user";
                context.Users.Add(u);
                context.SaveChanges();
                Session["messege"] = "Registration Successfull";
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(u);
            }
            
        }

    }
}
