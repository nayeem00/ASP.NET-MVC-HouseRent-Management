using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            Session["Login"] = null;
           
            Session["user"] = null;
            Session["CurrentLoginUser"] = null;
            Session["messege"] = "See you Again ";
            Session["FullName"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}
