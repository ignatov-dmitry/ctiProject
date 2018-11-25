using Aspose.Words;
using BusinessAnalytics.Models;
using db;
using Logics;
using Logics.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace BusinessAnalytics.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        private EFRepository<FinishedProducts> FinishedProducts;
        private ClaimLogic ClaimLogic;
        // GET: Claim
        public ClaimController()
        {
            ClaimLogic = new ClaimLogic();
        }
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();


            var user = Guid.Parse(User.Identity.GetUserId());
            return View(db.Claims.ToList());


        }
        [HttpGet]
        public int showsing(int id)
        {
            var db = new ApplicationDbContext();
            return db.Products.Where(x=>x.Id== id).Count();
        }
        [HttpGet]
        public ActionResult NewClaim()
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            var user = Guid.Parse(User.Identity.GetUserId());
            var db = new ApplicationDbContext();
            var users = db.Users.Find(User.Identity.GetUserId());
            ViewBag.State = users.InAndOut;
            var linq = (from l in FinishedProducts.Get()
                       select new FinishVM { NameVid = l.Name.Split(' ').First(),Img = l.Image}).ToList();
            var Fvm = linq.GroupBy(x => x.NameVid).Select(y => y.FirstOrDefault());
            foreach (var item in Fvm)
            {
                var d = FinishedProducts.Get(x => x.Name.Split(' ').First() == item.NameVid).ToList();
                foreach (var item1 in d)
                {
                    var vm = new VidFinishVM();
                    vm.NameVidFinish = item1.Name.Substring(item1.Name.IndexOf(' ') + 1);
                    vm.Id = item1.Id;
                    item.finishVMs.Add(vm);
                }
            }
            return View(Fvm.ToList());
        }
        [HttpGet]
        public JsonResult ViewTara(int id)
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            var json = from js in FinishedProducts.Get(x=>x.Id == id)
                       select new
                       {
                           Id = js.Id,
                           Name = js.Packaging.Name
                       };
            System.Diagnostics.Debug.WriteLine(json.ToString());
            return Json(json,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public string NameProd(int id)
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            return FinishedProducts.FindById(id).Name;
        }
        [HttpPost]
        public JsonResult NewClaim(int[] FinishProductId,int[] Count, double ObshSum)
        {
            var claim = new Claim();
            claim.ObshSum = ObshSum;
            claim.DateStart = DateTime.Now;
            foreach (var item in FinishProductId)
            {
                
                    claim.ClaimArray.Add(new ClaimArray() { FinishProductId = item});
                
            }
            var it = 0;
            foreach (var item in claim.ClaimArray)
            {
                item.Count = Count[it];
                ++it;
            }
            claim.IdClient = Guid.Parse(User.Identity.GetUserId());

            using (var db = new ApplicationDbContext())
            {
                claim.IdManager = Guid.Parse(db.Users.Where(x => x.UserName == "Manager").First().Id);
                db.Claims.Add(claim);
                db.SaveChanges();
            }
            return Json(claim, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize(Roles ="manager")]
        public ActionResult InUser()
        {
            return View(ClaimLogic.ClaimInList().OrderByDescending(x=>x.Id).ToList());
        }
        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult OutUser()
        {
            return View(ClaimLogic.ClaimOutList());
        }
        [HttpGet]
        public decimal CountFinish(int id)
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            decimal Count ;
            var db = new ApplicationDbContext();
            var user = db.Users.Find(User.Identity.GetUserId());
            var fin = FinishedProducts.FindById(id);
            if (user.InAndOut)
            {
                Count = fin.PriceIn;
            }
            else
            {
                Count = fin.PriceOut;
            }
            return Count;
        }
        [Authorize(Roles ="manager")]
        public ActionResult ClaimUser()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public ActionResult ClaimUserSignFalse()
        {
            var user = Guid.Parse(User.Identity.GetUserId());
            var claims = new List<Claim>();
            using (var db = new ApplicationDbContext())
            {
                var linq = db.Communications.Where(x=> x.ManagerId == user).ToList();
                foreach (var item in linq)
                {
                    var d = db.Claims.ToList();
                    claims.AddRange(d);

                }
               ViewBag.Cl = claims;
            }
            return PartialView(claims);
        }
        [Authorize(Roles = "manager")]
        public ActionResult PrinManager(int id)
        {
            ShipmentLogic ship = new ShipmentLogic();
            using (var db = new ApplicationDbContext())
            {
                if (!ship.PrihClaimNewShipment(id))
                {
                    var cl = db.Claims.Find(id);
                    cl.PrinManager = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("InUser");
                }
                else
                {
                    return HttpNotFound();
                }
                
            }
            
        }
        public int CountProdOld(int id)
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            int Count;
            var db = new ApplicationDbContext();
            var user = db.Users.Find(User.Identity.GetUserId());
            var fin = FinishedProducts.FindById(id);
            Count = fin.Count;
            return Count;
        }
        public ActionResult PrinClient(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var cl = db.Claims.Include(x=>x.ClaimArray).Where(x=>x.Id == id).First();
                cl.PrinClient =true;
               var sharr = db.shipment_Arrays.Where(x => x.ClaimId == id).First();
               var sh =  db.Shipment.Include(x=>x.shipment_Arrays).Where(x=>x.Id == sharr.IdShipment).First();
                int st = 0;
                foreach (var item in sh.shipment_Arrays)
                {
                    var cls = db.Claims.Find(item.ClaimId);
                    if (cls.PrinClient.Value)
                    {
                        ++st;
                    }
                    item.DatePrin = DateTime.Now;
                    item.Status = true;
                }
                if (sh.shipment_Arrays.Count == st)
                {
                    
                    sh.Status = true;
                    var auto = db.Auto.Find(sh.AutoId);
                    auto.Status = false;
                    db.Entry(sh).State = EntityState.Modified;
                    db.Entry(auto).State = EntityState.Modified;
                }
                foreach (var item in cl.ClaimArray)
                {
                    var fn = db.FinishedProducts.Find(item.FinishProductId);
                    fn.Count -= item.Count;
                    db.Entry(fn).State = EntityState.Modified;
                    db.SaveChanges();

                }
                db.Entry(cl).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //[Authorize(Roles = "manager")]
        //public ActionResult ClaimUserSignTrue()
        //{
        //    var user = Guid.Parse(User.Identity.GetUserId());
        //    var claims = new List<Claim>();
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var linq = from client in db.Communications
        //                   where client.ManagerId == user
        //                   select client;

        //        var s = linq.ToList();
        //        foreach (var item in s)
        //        {
        //            var d = db.Claims.Where(claim => claim.IdClient == item.ClientId && claim.Sign == true).ToList();
        //            claims.AddRange(d);

        //        }
        //    }
        //    return PartialView(claims);
        //}


        //[HttpGet]
        //public ActionResult ViewClaim(int? id)
        //{
        //    var db = new ApplicationDbContext();
        //    SelectList ProductId = new SelectList(db.Products, "Id", "Name");
        //    ViewBag.IdTovar = ProductId;
        //    UserOnline.online();
        //    try
        //    {
        //        var claim = db.Claims.Find(id);
        //        return View(claim);
        //    }
        //    catch (Exception)
        //    {

        //        return HttpNotFound();
        //    }
        //}


        //[HttpGet]
        //public ActionResult Podpis(int Id)
        //{
        //    OrderObj obj = new OrderObj();
        //    var db = new ApplicationDbContext();
        //    Order order = new Order();
        //    UserOnline.online();
        //    try
        //    {

        //        var claim = db.Claims.Find(Id);
        //        var user = db.Users.Where(x => x.Id == claim.IdClient.ToString()).First();
        //        var prod = db.Products.Where(x => x.Id == claim.IdProducts).FirstOrDefault();
        //        claim.Sign = true;
        //        order.Price = 0;
        //        order.ManagerId = Guid.Parse(User.Identity.GetUserId());
        //        order.ProductId = claim.IdProducts;
        //        order.Count = claim.Quantity;
        //        obj.Add(order, user.UserName);
        //        StaticUserObj.Add(new UserStatistics { UserId = Guid.Parse(User.Identity.GetUserId()), URL = string.Format(Url.Content("~/Sklad/Index")), Message = "Продан товар " + prod.Name + " с общей суммой " + (prod.Price * claim.Quantity) + " и количеством " + claim.Quantity, DateOfCompletion = DateTime.Now });
        //        db.SaveChanges();
        //        return View("ClaimUser");
        //    }
        //    catch (Exception)
        //    {

        //        return HttpNotFound();
        //    }
        //}
        //public ActionResult ActView()
        //{
        //    var user = Guid.Parse(User.Identity.GetUserId());
        //    List<Order> ord = new List<Order>();
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var linq = db.Claims.Where(x=>x.IdClient == user && x.Sign == true);

        //        var s = linq.ToList();
        //        foreach (var item in s)
        //        {
        //            ord.AddRange(db.Orders.Where(x => x.Count == item.Quantity && x.ProductId == item.IdProducts).ToList());
        //        }
        //    }

        //    return View(ord);
        //}
        //public void ExportInWord(int id)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    StringWriter sw = new StringWriter();

        //    sw.Write("Имя клиента \t");
        //    sw.Write("Названия продукта \t");
        //    sw.Write("Цена \t");
        //    sw.Write("Количество \t");
        //    sw.Write("Дата                                                              \t");

        //    Response.ClearContent();
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1251");
        //    Response.AddHeader("content-disposition", "attachment;filename=Акт.doc");
        //    Response.ContentType = "application/vnd.openxmlformatsofficedocument.wordprocessingml.documet";
        //    var order = db.Orders.ToList();
        //    sw.WriteLine();
        //    foreach (var item in order)
        //    {
        //        var clien = db.Clients.Where(x => x.Id == item.ClientId).ToList()[0];
        //        var prod = db.Products.Where(x => x.Id == item.ProductId).ToList()[0];
        //        sw.Write(clien.Name + "\t");
        //        sw.Write(prod.Name + "\t");
        //        sw.Write(item.Price + "\t");
        //        sw.Write(item.Count + "\t");
        //        sw.Write(item.Date + "\t");
        //        sw.WriteLine();
        //    }
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
    }
}