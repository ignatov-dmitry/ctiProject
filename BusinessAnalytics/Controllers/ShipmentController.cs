using Logics;
using Logics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessAnalytics.Controllers
{
    public class ShipmentController : Controller
    {
        private ShipmentLogic ship;
        private EFRepository<Auto> auto;
        private EFRepository<Claim> claim;
        private EFRepository<Shipment> ships;
        private EFRepository<Shipment_Array> shipall;
        private EFRepository<FinishedProducts> finish;
        private EFRepository<ClaimArray> claimarr;
        // GET: Shipment
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListShip()
        {
            auto = new EFRepository<Auto>(new ApplicationDbContext());
            claim = new EFRepository<Claim>(new ApplicationDbContext());
            ship = new ShipmentLogic();
            ships = new EFRepository<Shipment>(new ApplicationDbContext());
            shipall = new EFRepository<Shipment_Array>(new ApplicationDbContext());
            var db = new ApplicationDbContext();
            var json = from js in ships.Get() 
                       where js.PrinVyhOtpr== null
                       select new
                       {
                           auto = auto.FindById(js.AutoId.Value).Marka.ToString() + "-" + auto.FindById(js.AutoId.Value).Model.ToString(),
                           ar =  shipall.Get(x=>x.IdShipment == js.Id).ToArray(),
                           proc = ((db.Auto.Find(js.AutoId.Value).ObmKuz - js.MassOstAuto) / db.Auto.Find(js.AutoId.Value).ObmKuz*100).ToString("#.##")
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ViewClaim(int id)
        {
            var db = new ApplicationDbContext();
            var claim = db.Claims.Find(id).IdClient.ToString();
            return Json(db.Users.Find(claim).UserName, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Otpr(int id)
        {
            var db = new ApplicationDbContext();
            var sh = db.Shipment.Find(id);
            sh.PrinVyhOtpr = DateTime.Now;
            var auo = db.Auto.Find(sh.AutoId);
            auo.Status = true;
            db.Entry(auo).State = EntityState.Modified;
            db.Entry(sh).State = EntityState.Modified;
            db.SaveChanges();
           
           
            
            return Json(sh,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public void EditClaim(int id)
        {
            var db = new ApplicationDbContext();
            var sh = db.Shipment.Include(x=>x.shipment_Arrays).Where(x=>x.Id==id).First();
            foreach (var item in sh.shipment_Arrays)
            {
                var cl = db.Claims.Find(item.ClaimId.Value);
                cl.OtprAuto = DateTime.Now;
                db.Entry(cl).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public JsonResult ListSend()
        {
            auto = new EFRepository<Auto>(new ApplicationDbContext());
            claim = new EFRepository<Claim>(new ApplicationDbContext());
            ship = new ShipmentLogic();
            ships = new EFRepository<Shipment>(new ApplicationDbContext());
            shipall = new EFRepository<Shipment_Array>(new ApplicationDbContext());
            var db = new ApplicationDbContext();
            var json = from js in ships.Get()
                       where js.PrinVyhOtpr==null
                       select new
                       {
                           Id = js.Id, 
                           auto = auto.FindById(js.AutoId.Value).Marka.ToString() + "-" + auto.FindById(js.AutoId.Value).Model.ToString(),
                           ar = shipall.Get(x => x.IdShipment == js.Id).ToArray(),
                           proc = ((db.Auto.Find(js.AutoId.Value).ObmKuz - js.MassOstAuto) / db.Auto.Find(js.AutoId.Value).ObmKuz * 100).ToString("#.##")
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ArchiveShipment()
        {
            ViewBag.Title = "Архив";
            ViewBag.TitleOne = "Архив отгрузок";
            return View();
        }
        public JsonResult JsonArchive()
        {
            shipall = new EFRepository<Shipment_Array>(new ApplicationDbContext());
            ships = new EFRepository<Shipment>(new ApplicationDbContext());
            claim = new EFRepository<Claim>(new ApplicationDbContext());
            auto = new EFRepository<Auto>(new ApplicationDbContext());
            var db = new ApplicationDbContext();
            var json = from js in shipall.Get(x => x.Status == true)
                       select new
                       {
                           Id = ships.FindById(js.IdShipment).Id,
                          auto = auto.FindById(ships.FindById(js.IdShipment).AutoId).Marka + "-"  + auto.FindById(ships.FindById(js.IdShipment).AutoId).Model,
                          date = js.DatePrin,
                          user = db.Users.Find( claim.FindById(js.ClaimId).IdClient.ToString()).UserName,
                          IdClaim = js.ClaimId
                       };
            return Json(json,JsonRequestBehavior.AllowGet);
        }
        public JsonResult ViewClaimProduct(int id)
        {
            claim = new EFRepository<Claim>(new ApplicationDbContext());
            finish = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            claimarr = new EFRepository<ClaimArray>(new ApplicationDbContext());
            var json = from js in claim.GetWithInclude(x=>x.ClaimArray).Where(x=>x.Id == id).First().ClaimArray
                       select new {
                           ProdName =finish.FindById(js.FinishProductId).Name,
                           count = js.Count
                       };
            return Json(json,JsonRequestBehavior.AllowGet);
        }
    }
}