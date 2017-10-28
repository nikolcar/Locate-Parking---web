using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocateParking.Controllers
{
    public class HomeController : Controller
    {
        public static Models.HomeStatisticModels model;

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            model = await Models.HomeStatisticModels.Load();
            return View(model);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}