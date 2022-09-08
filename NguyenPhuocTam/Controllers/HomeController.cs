using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenPhuocTam.Models;
namespace NguyenPhuocTam.Controllers
{
    public class HomeController : Controller
    {
        BanHang2Entities db = new BanHang2Entities();
       /* public ActionResult Index()
        {
            var products = db.Products.Select(p => p).ToList();
    

            return View(products);
        }*/
        public ActionResult Index(string searchString)
        {
            var sp = from l in db.Products 
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.Productname.Contains(searchString));
            }
            if (Request.IsAjaxRequest()) return PartialView(sp);
            return View(sp);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult xemchitiet(int ID = 0)
        {
            var chitiet = db.Products.SingleOrDefault(n => n.Id == ID);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }

    }
}