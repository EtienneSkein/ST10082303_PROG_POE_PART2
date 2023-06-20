using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST10082303_PROG_POE_PART2.Controllers
{
    public class FarmerController : Controller
    {
        // GET: Farmer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddItem() 
        {
            return View();
        }
    }
}