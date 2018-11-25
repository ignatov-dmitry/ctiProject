using db;
using Logics.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class OtherLogic
    {

        public OtherLogic()
        {
        }
        public static string ViewName(string Id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Users.Find(Id).FIO;
            }
        } 
    }
}
