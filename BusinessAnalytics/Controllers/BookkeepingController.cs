using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logics;
using db;
namespace BusinessAnalytics.Controllers
{
    public class BookkeepingController : Controller
    {
        private BookkeepingLogic Logic = new BookkeepingLogic();
        // GET: Бухгалтеря
        public ActionResult Index()
        {
            return View(Logic.FinishedProductsList());
        }
        public ActionResult IndexOut()
        {
            return View(Logic.FinishedProductsList());
        }
        public JsonResult InCheck(FinishedProducts finishedProducts)
        {
            var db = new ApplicationDbContext();
            Logic.CheckIn(finishedProducts);
            var clm = db.FinishedProducts.Find(finishedProducts.Id);
            var cl = new {Name = clm.Name,Price = finishedProducts.PriceIn };
            return Json(cl,JsonRequestBehavior.AllowGet);
        }
        public JsonResult OutCheck(FinishedProducts finishedProducts)
        {
            Logic.CheckOut(finishedProducts);
            var cl = new { Name = finishedProducts.Name, Price = finishedProducts.PriceOut };
            return Json(cl, JsonRequestBehavior.AllowGet);
        }
    }
}