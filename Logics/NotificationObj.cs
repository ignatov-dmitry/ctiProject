using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
namespace Logics
{
    [Authorize]
    static public class NotificationObj
    {
        static public void Add(Notification name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Notifications.Add(name);
            db.SaveChanges();
        }
        static public void Remove(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var lint = db.Notifications.Find(id);
            db.Notifications.Remove(lint);
            db.SaveChanges();
        }
        static public void ListRemove(int[] id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            foreach (var item in id)
            {
                var not = db.Notifications.Find(item);
                db.Notifications.Remove(not);
                db.SaveChanges();
            }
        }
        static public void TaskSrok(Guid id, string url)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var linq = from task in db.TasksManager.ToList()
                       where (task.UserId == id && task.DateEnd.Day + 1 == DateTime.Now.Day && task.Status == false) || (task.UserId == id && task.DateEnd.Day == DateTime.Now.Day && task.Status == false)
                       select task;
            if (linq.Count() > 0)
            {
                TaskObj obj = new TaskObj();
                foreach (var item in linq)
                {
                    Add(new Notification { AspNetUserId = id, Url = url, Status = false, Message = "Уважаемый " + obj.ViewUserName(id).UserName + " у вас заканчиваеться срок задачи." });
                }
            }


        }
        static public void TaskNot(string url)
        {
            TaskObj obj = new TaskObj();
            foreach (var item in obj.ViewsListObj())
            {
                if (item.Status == false && item.DateEnd.Date > DateTime.Now.Date)
                {
                    Add(new Notification { AspNetUserId = Guid.Parse("3950eec4-39b4-4b16-a4e7-c7e7e3f978c8"),Status = false, Url = url,Message = "Данный пользователь " + obj.ViewUserName(item.UserId).UserName + " не выполнил задания."});
                }
            }
        }
        static public List<Notification> ViewsListObj()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var linq = from lin in db.Notifications.ToList()
                       where lin.AspNetUserId == Guid.Parse(HttpContext.Current.User.Identity.GetUserId())
                       select lin;
            return linq.ToList();
        }

        static public Notification ViewsObj(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var jet = from link in db.Notifications
                      where link.id == id
                      select link;
            var a = db.Notifications.Find(id);
            a.Status = true;
            db.SaveChanges();
            return jet.FirstOrDefault();
        }
        static public  int Count()
        {
            var user = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            using (var ds = new ApplicationDbContext())
            {
                var linq = from obj in ds.Notifications
                           where obj.AspNetUserId == user
                           select obj;
                return linq.Count();
            }
        }
        static public List<Notification> ViewList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Guid d = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            

            
                var list = from li in db.Notifications
                           orderby li.id descending
                           where li.Status == false && db.Notifications.Count() <= 3 && li.AspNetUserId == d
                           select li;
            
            return list.ToList();
            
            
            
        }
        static public  int CountNew()
        {
            Guid d = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            using (var ds = new ApplicationDbContext())
            {
                var lin = from list in ds.Notifications.ToList()
                          where (list.Status == false && list.AspNetUserId == d)
                          select list;

                return lin.Count();
            }
        }
        static public byte[] ViewImg(string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var linq = from li in db.Users
                       where li.UserName == name
                       select li.Image;

            return linq.First();
        }
    }
}
