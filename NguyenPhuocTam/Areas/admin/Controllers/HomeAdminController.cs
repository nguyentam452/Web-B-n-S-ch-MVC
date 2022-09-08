using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenPhuocTam.Models;

namespace NguyenPhuocTam.Areas.admin.Controllers
{
    public class HomeAdminController : Controller
    {
        BanHang2Entities db = new BanHang2Entities();
        // GET: admin/Login
        public ActionResult Index()
        {
           
            return View();
        }
    }
}