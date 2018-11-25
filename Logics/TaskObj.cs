using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Logics
{
    [Authorize]
    public class TaskObj:ILogic<TasksManager>
    {
        public dynamic ViewUser;
        private ApplicationDbContext db;
        public TaskObj()
        {
            db = new ApplicationDbContext();
        }

        public void Add(TasksManager name)
        {
            db.TasksManager.Add(name);
            db.SaveChanges();
        }

        public void Edit(TasksManager name)
        {
            var tsk = db.TasksManager.Find(name.Id);
            tsk.Name = name.Name;
            tsk.Status = name.Status;
            tsk.Text = name.Text;
            tsk.User = name.User;
            tsk.UserId = name.UserId;
            tsk.DateEnd = name.DateEnd;
            tsk.DateStart = name.DateStart;
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            var tas = db.TasksManager.Find(id);
            db.TasksManager.Remove(tas);
            db.SaveChanges();
        }

        public List<TasksManager> ViewsListObj()
        {
            
            return db.TasksManager.ToList();
        }

        public TasksManager ViewsObj(int id)
        {
            return db.TasksManager.Find(id);
        }
        public ApplicationUser ViewUserName(Guid id)
        {
            var us = db.Users.ToList();
            return db.Users.Where(x => x.Id == id.ToString()).First();
        }
        public Array GetTaskCalendar()
        {
            var set = from task in db.TasksManager
                      select new
                      {
                          date = task.DateStart.Day + "-" + task.DateStart.Month + "-" + task.DateStart.Year + " " + "10" + ":" + "20" + ":" + "30",
                          title = task.Name,
                          description = task.Text,
                          url = "/"
                      };
            return set.ToArray();
        }
    }
}
