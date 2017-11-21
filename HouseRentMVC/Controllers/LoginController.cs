using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Login"]!=null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.messege = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string username = collection["username"];
            string password = collection["password"];

            HouseRentDBContext context = new HouseRentDBContext();
            User u = context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if(u != null)
            {
                Session["Login"] = true;
                Session["FullName"] = u.FullName;
                Session["user"] = u.UserStatus;
                Session["CurrentLoginUser"] = u;
                Session["messege"] = "Welcome " + Session["FullName"];


                return RedirectToAction("Index", "Home");

            }
            else{
                ViewBag.messege = "Invalid UserName Or PassWord";
                return View();

            }
            

            
        }

    }
}
