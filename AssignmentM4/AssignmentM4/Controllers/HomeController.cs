using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentM4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();  
        }

        public ActionResult About()
        {
            ViewBag.Message = "Skincare Routines";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Skincare Treatment Centers";

            return View();
        }
    }
}