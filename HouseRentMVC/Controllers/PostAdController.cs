using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class PostAdController : Controller
    {
        //
        // GET: /PostAd/
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                
                    return RedirectToAction("Index", "Login");
                

            }
            
            HouseRentDBContext context = new HouseRentDBContext();

            List<SelectListItem> Arealist = new List<SelectListItem>();
            foreach (Area a in context.Areas)
            {
                SelectListItem li = new SelectListItem();
                li.Text = a.AreaName;
                li.Value = a.Id.ToString();
                Arealist.Add(li);
            }

            ViewBag.Arealist = Arealist;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Post p)
        {
            string fileName = null;
            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff"), fileName);
                    string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                }
            }
            else
            {
                fileName = "default.jpg";
            }
            string Category = Request["Category[]"];
            int did = Convert.ToInt32(Request["did"]);
            HouseRentDBContext context = new HouseRentDBContext();
            
           

            User u = (User)Session["CurrentLoginUser"];
            p.CreatedBy = u.Id;
            p.CreatedDate = DateTime.Now;
            p.ApprovedDate = DateTime.Now;
            p.UpdatedDate = DateTime.Now;
            p.Image = fileName;
            p.Category = Category;
            p.HireStatus = 0;
            p.Approval = 0;
                
            
            
            
            if (ModelState.IsValid)
            {
                

                context.Posts.Add(p);
                context.SaveChanges();
                Session["messege"] = "Successfully Posted Ad";


                return RedirectToAction("Index", "Home");
            }
            else
            {
                

                List<SelectListItem> Arealist = new List<SelectListItem>();
                foreach (Area a2 in context.Areas)
                {
                    SelectListItem li = new SelectListItem();
                    li.Text = a2.AreaName;
                    li.Value = a2.Id.ToString();
                    Arealist.Add(li);
                }

                ViewBag.Arealist = Arealist;
                return View(p);
            }
            
            
            
        }


    }
}
