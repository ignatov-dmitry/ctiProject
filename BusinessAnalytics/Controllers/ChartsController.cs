using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db;
using Logics;
using BusinessAnalytics.Models;
using Microsoft.AspNet.Identity;
namespace BusinessAnalytics.Controllers
{
    public class ChartsController : Controller
    {
        static SeasonalityObj obj = new SeasonalityObj();
       
        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult json()
        {
            return Json(obj.ViewsListObj(), JsonRequestBehavior.AllowGet);
        }
    }
}