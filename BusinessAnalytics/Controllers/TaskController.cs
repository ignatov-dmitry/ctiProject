using db;
using Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessAnalytics.Models;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;

namespace BusinessAnalytics.Controllers
{
    public class TaskController : Controller
    {
        static TaskObj obj = new TaskObj();
        public ActionResult Index()
        {
            NotificationObj.TaskSrok(Guid.Parse(User.Identity.GetUserId()), string.Format(Url.Content("~/Task/Calendar")));
            UserOnline.online();
            using (var db = new ApplicationDbContext())
            {
                ViewBag.UserList = db.Users.ToList();
            }
            
            return View(obj.ViewsListObj());
        }
        [Authorize(Roles = "admin")]
        public ActionResult Add(string user,string name,string Messege,DateTime DateStart,DateTime DateEnd)
        {
            UserOnline.online();
            obj.Add(new TasksManager() {UserId = Guid.Parse(user),Name = name,Text = Messege,DateStart = DateStart,DateEnd = DateEnd,Status = false});
            NotificationObj.Add(new Notification {AspNetUserId = Guid.Parse(User.Identity.GetUserId()),Url=string.Format(Url.Content("~/Task/Calendar") ),Status = false,Message = "Вам назначено новое задание!"});
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id)
        {
            UserOnline.online();
            obj.Remove(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                ViewBag.UserList = db.Users.ToList();
            }
            return View(obj.ViewsObj(id));
        }
        [HttpPost]
        public ActionResult Edit(TasksManager task)
        {
            UserOnline.online();
            obj.Edit(task);
            return RedirectToAction("Index");
        }
        public ActionResult Calendar()
        {
            UserOnline.online();
            return View();
        }
        [HttpGet]
        public ActionResult ShowTask(Guid id)
        {
            var db = new ApplicationDbContext();
            ViewBag.UserList = db.Users.ToList();
            var set = from user in db.TasksManager
                      where user.UserId == id
                      select user;
            return View(set.ToList()[0]);
        }
        [HttpPost]
        public ActionResult ShowTask(TasksManager od,string check)
        {
            TaskObj task = new TaskObj();
            if (check == "on")
            {
                od.Status = true;
                NotificationObj.Add(new Notification {AspNetUserId = Guid.Parse("3950eec4-39b4-4b16-a4e7-c7e7e3f978c8"),Url=Url.Content("~/Task/Index"),Status = false,Message = "Данный пользователь"+ obj.ViewUserName(od.UserId).UserName + " выполнил задания." });
            }
            obj.Edit(od);
            return RedirectToAction("Calendar");
        }
        public JsonResult ShowCalendar()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var set = from task in db.TasksManager
                      where task.Status == false
                      select new
                      {
                          date = task.DateStart.Year + "-" + task.DateStart.Month + "-" + task.DateStart.Day + " " + "10" + ":" + "20" + ":" + "30",
                          title = task.Name,
                          description = task.Text,
                          url = "/Task/ShowTask/" + task.UserId
                      };
            return Json(set.ToList(), JsonRequestBehavior.AllowGet);
        }

        
    }
}