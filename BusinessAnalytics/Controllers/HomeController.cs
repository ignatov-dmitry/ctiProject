using db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BusinessAnalytics.Models;
using Logics;
using System.Threading.Tasks;
using IndexViewModel = Logics.IndexViewModel;

namespace BusinessAnalytics.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
            
        public ActionResult Index()
        {
            NotificationObj.TaskNot(Url.Content("~/Task/Index"));
            NotificationObj.TaskSrok(Guid.Parse(User.Identity.GetUserId()), string.Format(Url.Content("~/Task/Calendar")));
            UserOnline.online();
            var db = new ApplicationDbContext();
            var newProducts = from newprod in db.Products.ToList()
                              //where (DateTime.Now - newprod.Date <= new TimeSpan(7, 0, 0, 0)) 
                              select newprod;
            
           
            var events = db.Events.ToList();
            var user = Guid.Parse(User.Identity.GetUserId());
            
            
           
            ViewBag.newProducts = newProducts.Count();
            
            ViewBag.totalSum = string.Format("{0:N0}", 4);
            ViewBag.events = events;
            
            return View();
        }
        
        public ActionResult Users()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                           .GetUserManager<ApplicationUserManager>();
            using (var context = new ApplicationDbContext())
            {
                List<UserModel> user = new List<UserModel>();
                
               
                foreach (var item in context.Users.ToList())
                {
                    
                    UserModel ur = new UserModel();
                    ur.AspNetUserId =Guid.Parse(item.Id);
                    ur.Name = item.UserName;
                    var role = item.Roles.First().RoleId;
                    if (item.Id == "3e9b44c7-ed96-4b69-b28d-ab7cf1a5db4c")
                    {
                        ur.Role = "admin";
                    }
                    else
                    {
                        ur.Role = context.Roles.Where(x => x.Id == role).First().Name;
                    }
                    
                    user.Add(ur);
                }
                
                return View(user);
            }
           
        }
        public ActionResult UsersClient()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                           .GetUserManager<ApplicationUserManager>();
            using (var context = new ApplicationDbContext())
            {
                List<UserModel> user = new List<UserModel>();
                List<UserModel> client = new List<UserModel>();
                var manager = Guid.Parse(User.Identity.GetUserId());
                var ue = context.Communications.Where(x => x.ManagerId == manager).ToList();
                var role = context.Roles.Where(x => x.Name == "Client").First();
                //foreach (var cl in context.Users.ToList())
                //{
                //    foreach (var f in ue)
                //    {
                //        if (cl.Id == f.ClientId.ToString())
                //        {
                //            var role = cl.Roles.First().RoleId;
                //            client.Add(new UserModel() { Name = cl.UserName, Role = context.Roles.Where(x => x.Id == role).First().Name,AspNetUserId= Guid.Parse(cl.Id) });

                //        }
                //    }
                //}
                foreach (var item in context.Users.ToList())
                {
                    foreach (var items in item.Roles.Where(x=>x.RoleId == role.Id).ToList())
                    {
                        if (items.UserId == item.Id)
                        {
                            client.Add(new UserModel() { AspNetUserId = Guid.Parse(item.Id), Company = item.Company });
                        }
                    }
                    
                    
                }
                return View(client);
            }

        }
        public JsonResult ViewClaim(string IdClient,DateTime from,DateTime to, int page = 1)
        {
            int pageSize = 3;
            var db = new ApplicationDbContext();
            var claim = db.Claims.Where(x => x.IdManager == Guid.Parse(IdClient)).ToList();
            var claimPerPages = db.Claims.Where(x => x.IdClient == Guid.Parse(IdClient)).ToList().Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = claim.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Claim = claimPerPages };
            return Json(ivm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewUser(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Find(id);
            return View(user);
        }
        public ActionResult AddUser()
        {
            return Redirect("~/Account/Register");
        }
        public ActionResult AddClient()
        {
            return Redirect("~/Account/RegisterClient");
        }

        public ActionResult Role()
        {
            return View();
        }

    }

}
