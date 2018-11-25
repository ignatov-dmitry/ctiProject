using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Web.Mvc;

namespace Logics
{
    [Authorize]
    public static class MiniChat
    {
        
        public static List<Read> Users()
        {
            var db = new ApplicationDbContext();
            var user = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            var union = from u in db.Users.ToList()
                        select new
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Role = u.Roles.Select(p => p.RoleId).Single()
                        };



            var message = union.ToList().Where(p => p.Id != user.ToString() && p.Role != "a969494c-8e16-407c-813d-6a7e6d57a8db").GroupJoin(
                db.ChatMessages.ToList(),
                u => Guid.Parse(u.Id),
                m => m.UserSetId,
                (usr, msg) => new Read()
                {
                    User = Guid.Parse(usr.Id),
                    Count = msg.Where(x => x.Read == false && x.UserGetId == user).Select(p => p.Text).Count(),
                    Date = db.ChatMessages.ToList().Where(x => (x.UserSetId == Guid.Parse(usr.Id) && x.UserGetId == user) || (x.UserSetId == user && x.UserGetId == Guid.Parse(usr.Id))).Select(p => p.Date).LastOrDefault()

                }).OrderByDescending(p=>p.Date);
            return message.ToList();
        }

        public static List<Read> PersonalClient()
        {
            var db = new ApplicationDbContext();
            var user = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            var personalClient = db.Communications.ToList().Where(p => p.ManagerId == user).GroupJoin(
                db.ChatMessages.ToList(),
                u => u.ManagerId,
                m => m.UserSetId,
                (com, msg) => new Read()
                {

                    User = com.ClientId,
                    Count = msg.Where(x => x.Read == false && x.UserGetId == user).Select(p => p.Text).Count(),
                    Date = db.ChatMessages.ToList().Where(x => (x.UserSetId == com.ClientId && x.UserGetId == user) || (x.UserSetId == user && x.UserGetId == com.ClientId)).Select(p => p.Date).LastOrDefault()
                }).OrderByDescending(p => p.Date);
            return personalClient.ToList();
        }

        public static List<Read> PersonalManager()
        {
            var db = new ApplicationDbContext();
            var user = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            var personalManager = db.Communications.ToList().Where(p => p.ClientId == user).GroupJoin(
                db.ChatMessages.ToList(),
                u => u.ClientId,
                m => m.UserSetId,
                (com, msg) => new Read()
                {
                    User = com.ManagerId,
                    Count = msg.Where(x => x.Read == false && x.UserGetId == user).Select(p => p.Text).Count(),
                    Date = db.ChatMessages.ToList().Where(x => (x.UserSetId == com.ManagerId && x.UserGetId == user) || (x.UserSetId == user && x.UserGetId == com.ManagerId)).Select(p => p.Date).LastOrDefault()
                }).OrderByDescending(p => p.Date);

            return personalManager.ToList();
        }
    }
}
