using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db;
using Logics;
using Microsoft.AspNet.Identity;
namespace BusinessAnalytics.Controllers
{
    public class EventsController : Controller
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult add(Event events, string Name, string Message)
        {

            events.Date = DateTime.Now;
            events.Name = Name;
            events.Message = Message;
            db.Events.Add(events);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}