using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRentMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
    [HttpGet]
        public ActionResult Index()
        {
            string msg = (string)Session["messege"];
            Session["messege"] = "";
            ViewBag.message = msg;
            ViewBag.SearchBoxValue = "";
            HouseRentDBContext context = new HouseRentDBContext();
            var result = from e in context.Posts
                         join d in context.Areas
                         on e.AreaId equals d.Id
                         where e.Approval == 1
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
                             AreaName = d.AreaName
                         };
            



            ViewBag.PostList = result;

            ViewBag.SuggestPost = null;


            return View();
            

        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            string SearchBox = f["SearchBox"];
            string priceRange = f["priceRange"];
            string RoomNumber = f["RoomNumber"];
            string Category = f["Category"];
            string Ip = Request.UserHostAddress;
            HouseRentDBContext context = new HouseRentDBContext();
            SearchHistory s = new SearchHistory();
            s.Ip = Ip;
            s.KeyWord = SearchBox;
            s.PriceFilter = priceRange;
            s.RoomFilter = RoomNumber;
            s.CategoryFilter = Category;
            s.Time = DateTime.Now;
            context.SearchHistories.Add(s);
            context.SaveChanges();
            ViewBag.SearchBoxValue = SearchBox;

            string[] price = priceRange.Split('-');
            int temp1 = Int32.Parse(RoomNumber);
            int temp2 = Int32.Parse(price[0]);
            int temp3 = Int32.Parse(price[1]);


            var result = context.Posts.Where(x => x.Approval == 1)
                .Join(context.Areas,
                e => e.AreaId,
                d => d.Id,
                (e, d) => new mypost
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
                    AreaName = d.AreaName
                }).Where(p => p.AreaName.Contains(SearchBox))
                .Where(p => p.Bedroom > temp1)
                .Where(x => x.Rent > temp2  && x.Rent < temp3)
                .Where(p => p.Category.Contains(Category));


              ViewBag.PostList = result;
              Area a = context.Areas.SingleOrDefault(x => x.AreaName == SearchBox);
              if(a !=null)
              {
                  List<mypost> suggestlist = new List<mypost>();
                  string[] Sareas = a.SurroundingArea.Split(',');
                  foreach(string temparea in Sareas){
                      var tempresult = from e in context.Posts
                                   join d in context.Areas
                                   on e.AreaId equals d.Id
                                   where e.Approval == 1
                                   where d.AreaName == temparea
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
                                       AreaName = d.AreaName
                                   };
                      foreach (var item in (IEnumerable<mypost>)tempresult)
                      {
                          
                          suggestlist.Add(item);
                      }

                  }
                  ViewBag.SuggestPost = suggestlist;

              }
              else
              {
                  ViewBag.SuggestPost = null;
              }



            string msg = (string)Session["messege"];
            Session["messege"] = "";
            ViewBag.message = msg;
            return View();


        }

        public JsonResult AutoComplete(string term)
        {
            HouseRentDBContext context = new HouseRentDBContext();
            List<string> custnames = new List<string>();
            List<Area> clist = context.Areas.Where(c => c.AreaName.Contains(term)).ToList();
            foreach (Area c in clist)
            {
                custnames.Add(c.AreaName);
            }

            return Json(custnames, JsonRequestBehavior.AllowGet);
        }
        

    }
}
