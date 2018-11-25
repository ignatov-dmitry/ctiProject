using Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessAnalytics.Models;
using System.Threading.Tasks;
using db;
using Microsoft.AspNet.Identity;
namespace BusinessAnalytics.Controllers
{
    public class NotificationController : Controller
    {
        public ActionResult Index()
        {
            
            UserOnline.online();
            return View(NotificationObj.ViewsListObj());
        }
        public ActionResult ViewId(int id)
        {
            return View(NotificationObj.ViewsObj(id));
        }
        public JsonResult ViewL()
        {
            return Json(NotificationObj.ViewList(),JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserOnline.online();
            try
            {
                return View(NotificationObj.ViewsObj(id));
            }
            catch (Exception)
            {

                return HttpNotFound();
            }


        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NotificationObj.Remove(id);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAll(int[] id)
        {
            NotificationObj.ListRemove(id);
            return RedirectToAction("Index");
        }
        public int ColNew()
        {
            var d = Guid.Parse(User.Identity.GetUserId());
            using (var ds = new ApplicationDbContext())
            {
                var lin = from list in ds.Notifications.ToList()
                          where (list.Status == false && list.AspNetUserId == d)
                          select list;

                return lin.Count();
            }
            
        }
        public int Col()
        {

            var user = Guid.Parse(User.Identity.GetUserId());
            using (var ds = new ApplicationDbContext())
            {
                var linq = from obj in ds.Notifications
                           where obj.AspNetUserId == user && obj.Status == false
                           select obj;
                return linq.Count();
            }
           
        }

    }
}