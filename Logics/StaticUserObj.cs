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
    public static class StaticUserObj
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public static void Add(UserStatistics obj)
        {
            db.UserStatics.Add(obj);
            db.SaveChanges();
        }
        public static UserStatistics ViewObj(int id)
        {
            var obj = db.UserStatics.Find(id);
            return obj;
        }
        public static List<UserStatistics> ListViewObj()
        {
            return db.UserStatics.ToList();
        }
        public static void DeleteObj(int id)
        {
            var obj = db.UserStatics.Find(id);
                db.UserStatics.Remove(obj);
                db.SaveChanges();
        }
    }
}
