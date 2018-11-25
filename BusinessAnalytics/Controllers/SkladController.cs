using db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Logics;
using BusinessAnalytics.Models;
using Logics.Hubs;
using Microsoft.AspNet.SignalR;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Logics.Repository;
using System.Threading.Tasks;

namespace BusinessAnalytics.Controllers
{

    public class SkladController : Controller
    {
        SkladLogic sklad = new SkladLogic();
        OtherLogic other = new OtherLogic();
        private EFRepository<RawMaterialStatistics> statistic;
        private EFRepository<Product> product;
        private EFRepository<Raw_Product> Raw_Product;
        private EFRepository<Gild> Gild;
        private EFRepository<Gild_GildStatic> Gild_GildStatic;
        private EFRepository<GildStatic> GildStatic;
        private EFRepository<Packaging> Packaging;
        private EFRepository<Condiments> Condimentss;
        private EFRepository<CondimentStatic> CondimentStatic;
        private EFRepository<Condiments_Static> Condiments_Static;
        private EFRepository<FinishedProducts> FinishedProducts;
        private EFRepository<FinishedGoodsStatistics> FinishedGoodsStatistics;
        private EFRepository<FinishedProducts_FinishedGoodsStatistics> FinishedProducts_FinishedGoodsStatistics;
        private EFRepository<FinishGildArray> finArray;
        /// <summary>
        /// Список сырья
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            UserOnline.online();
            return View(sklad.GetProductList());
        }
        /// <summary>
        /// Список сырья и приход нового:- Вывод
        /// </summary>
        /// <returns></returns>
        public ActionResult InSklad()
        {
            UserOnline.online();
            ViewBag.Syr = new SelectList(sklad.GetProductList(), "Id", "Name");
            return View(sklad.GetProductList());
        }
        [HttpGet]
        public JsonResult DropSyr()
        {
            var json = from js in sklad.GetProductList()
                       select new
                       {
                           Id = js.Id,
                           Name = js.Name
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DropSpec()
        {
            Condimentss = new EFRepository<Condiments>(new ApplicationDbContext());
            var json = from js in Condimentss.Get()
                       select new
                       {
                           Id = js.Id,
                           Name = js.Name
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DropFinish()
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            var json = from js in FinishedProducts.Get()
                       select new
                       {
                           Id = js.Id,
                           Name = js.Name
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Отдает Json сырья
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult InSkladJson()
        {
            product = new EFRepository<Product>(new ApplicationDbContext());
            Raw_Product = new EFRepository<Raw_Product>(new ApplicationDbContext());
            statistic = new EFRepository<RawMaterialStatistics>(new ApplicationDbContext());
            var json = from js in statistic.Get()
                       orderby js.Id descending
                       where js.Date.Value.Day == DateTime.Now.Day && js.Date.Value.Month == DateTime.Now.Month && js.Date.Value.Year == DateTime.Now.Year
                       select new
                       {
                           Id = js.Id,
                           Name = product.FindById(Raw_Product.Get(x => x.RawId == js.Id).First().ProductId.Value).Name,
                           Time = js.Date.Value.ToShortTimeString(),
                           Count = js.Count == 0 ? 1 : js.Count,
                           VesName = "кг."
                       };
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        //Добавления нового Сырья
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// Добавляет новое сырье
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Add(Product product)
        {
            this.product = new EFRepository<Product>(new ApplicationDbContext());
            UserOnline.online();

            await sklad.AddProduct(product);

            var user = Guid.Parse(User.Identity.GetUserId());
            NotificationObj.Add(new Notification { AspNetUserId = user, Status = false, Url = string.Format(Url.Content("~/Sklad/Index")), Message = "Добавлен товар на склад " + product.Name });
            StaticUserObj.Add(new UserStatistics { UserId = Guid.Parse(User.Identity.GetUserId()), URL = string.Format(Url.Content("~/Sklad/Index")), Message = "Добавлен товар " + product.Name + " с ценой " + " и количеством " + product.Count, DateOfCompletion = DateTime.Now });
            return Json(product, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Приход сырья на склад
        /// </summary>
        /// <param name="prod"></param>
        [HttpPost]
        public async Task<JsonResult> Prih(Product prod)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                await sklad.PrihodSyr(prod);
                prod.Name = db.Products.Find(prod.Id).Name;
                return Json(prod, JsonRequestBehavior.AllowGet);
            }
        }


        //Удаления сырья
        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserOnline.online();
            try
            {
                return View();
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {//add
            UserOnline.online();
            try
            {

            }
            catch (Exception)
            {

                return HttpNotFound();
            }


            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var db = new ApplicationDbContext();
            var products = db.Products;

            UserOnline.online();
            try
            {

                return View();
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult Edit(Product products)
        {//add
            UserOnline.online();

            StaticUserObj.Add(new UserStatistics { UserId = Guid.Parse(User.Identity.GetUserId()), URL = string.Format(Url.Content("~/Sklad/Index")), Message = "Изменен товар " + products.Name + " с ценой " + " и количеством " + products.Count, DateOfCompletion = DateTime.Now });
            return RedirectToAction("Index");
        }
        public ActionResult GildView()
        {
            ViewBag.Syr = new SelectList(sklad.GetProductList(), "Id", "Name");
            ViewBag.Con = new SelectList(sklad.CondimentsViewList(), "Id", "Name");
            return View();
        }
        public JsonResult GildJson()
        {
            product = new EFRepository<Product>(new ApplicationDbContext());
            Gild_GildStatic = new EFRepository<Gild_GildStatic>(new ApplicationDbContext());
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            var json = from js in sklad.gildStaticsList()
                       orderby js.Id descending
                       where Gild.Get(d => d.Id == js.Gild_GildStatics.Where(x => x.GildStaticId == js.Id).First().GildId.Value).First().CondimentsId == null
                       select new
                       {
                           Name = product.FindById(Gild.FindById(Gild_GildStatic.Get(x => x.GildStaticId == js.Id).First().GildId.Value).ProductId.Value).Name,
                           Count = js.CountGild,
                           Time = js.Date.ToShortTimeString()
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GildSpecJson()
        {
            product = new EFRepository<Product>(new ApplicationDbContext());
            Condimentss = new EFRepository<Condiments>(new ApplicationDbContext());
            Gild_GildStatic = new EFRepository<Gild_GildStatic>(new ApplicationDbContext());
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            var json = from js in sklad.gildStaticsList()
                       orderby js.Id descending
                       where Gild.Get(d => d.Id == js.Gild_GildStatics.Where(x => x.GildStaticId == js.Id).First().GildId.Value).First().ProductId == null
                       select new
                       {
                           Name = Condimentss.FindById(Gild.FindById(Gild_GildStatic.Get(x => x.GildStaticId == js.Id).First().GildId.Value).CondimentsId.Value).Name,
                           Count = js.CountGild,
                           Time = js.Date.ToShortTimeString()
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddGild()
        {
            ViewBag.syr = new SelectList(sklad.GetProductList(), "Id", "Name");
            return View();
        }
        [HttpGet]
        public int ShowProductCount(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var d = int.Parse(db.Products.Find(id).Count.ToString());
                return d;
            }

        }
        [HttpPost]
        public async Task<JsonResult> AddGild(Gild gild)
        {
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            product = new EFRepository<Product>(new ApplicationDbContext());
            if (gild.Id != 0)
            {
                gild.Name = Gild.FindById(gild.Id).Name;
            }
            else
            {
                gild.Name = product.FindById(gild.ProductId.Value).Name;
            }
            gild.State = sklad.GildAdd(gild, 0);
            return Json(gild, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public int showId(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var gld = db.Gilds.Where(x => x.ProductId == id).First();
                var glds = db.Gild_GildStatics.Where(x => x.GildId == gld.Id).OrderByDescending(x => x.Id).First();
                return int.Parse(db.GildStatics.Find(glds.GildStaticId).CountGild.ToString());
            }

        }
        [HttpGet]
        public int showIdSpec(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var gld = db.Gilds.Where(x => x.CondimentsId == id).First();
                var glds = db.Gild_GildStatics.Where(x => x.GildId == gld.Id).OrderByDescending(x => x.Id).First();
                return int.Parse(db.GildStatics.Find(glds.GildStaticId).CountGild.ToString());
            }

        }
        [HttpGet]
        public int showIdFinish(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var gld = db.FinishedProducts.Where(x => x.PackagingId == id).First();
                var glds = db.FinishedProducts_FinishedGoodsStatistics.Where(x => x.FinishedProductsId == gld.Id).OrderByDescending(x => x.Id).First();
                return int.Parse(db.FinishedGoodsStatistics.Find(glds.FinishedGoodsStatisticsId).CountPack.ToString());
            }

        }
        [HttpPost]
        public async Task<JsonResult> AddSpecGild(Gild gild, double CountCon)
        {
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            Condimentss = new EFRepository<Condiments>(new ApplicationDbContext());
            if (gild.Id != 0)
            {
                gild.Name = Gild.FindById(gild.Id).Name;
            }
            else
            {
                gild.Name = Condimentss.FindById(gild.CondimentsId.Value).Name;
            }
            gild.Count = int.Parse(CountCon.ToString());
            gild.State = sklad.GildAdd(gild, CountCon);
            return Json(gild, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> PrihGild(Gild gild, double CountCon)
        {
            await sklad.ModGild(gild, CountCon);
            return Json(gild, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PackagingView()
        {
            return View();
        }
        public JsonResult PackagingViewJson()
        {
            var json = from js in sklad.PackagingList()
                       where js.Count > 0
                       orderby js.Id descending
                       select new
                       {
                           Id = js.Id,
                           Name = js.Name,
                           Count = js.Count
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult PrihInSklad()
        {
            ViewBag.Pack = new SelectList(sklad.PackagingList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> PrihInSklad(Packaging pack)
        {
            Packaging = new EFRepository<Packaging>(new ApplicationDbContext());
            await sklad.PrihodPack(pack);
            pack.Name = Packaging.FindById(pack.Id).Name;
            return Json(pack, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewProduct()
        {
            return View(sklad.GetFinishedProducts());
        }
        [HttpGet]
        public int ShowPackCount(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return int.Parse(db.Packagings.Find(id).Count.ToString());
            }

        }
        public int ShowPackCondiments(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return int.Parse(db.Condiments.Find(id).Count.ToString());
            }

        }
        [HttpGet]
        public ActionResult AddPack()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddPack(Packaging pack)
        {
            await sklad.PackagingAdd(pack);
            return Json(pack, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FinishedProduct()
        {
            ViewBag.Prod = new SelectList(sklad.GetFinishedProducts(), "Id", "Name");
            return View();
        }
        [HttpGet]
        public ActionResult AddFinishedProduct()
        {
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            ViewBag.gildSyr = new SelectList(Gild.Get(x => x.ProductId != null).ToList(), "Id", "Name");
            ViewBag.gildSpec = new SelectList(Gild.Get(x => x.CondimentsId != null).ToList(), "Id", "Name");
            ViewBag.Pack = new SelectList(sklad.PackagingList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddFinishedProduct(FinishedProducts finishedProducts, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                finishedProducts.Image = imageData;
            }
            sklad.AddFinishedProduct(finishedProducts);
            return RedirectToAction("AddAll");
        }
        [HttpGet]
        public ActionResult InFinishedProduct()
        {
            ViewBag.Prod = new SelectList(sklad.GetFinishedProducts(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public JsonResult InFinishedProduct(FinishedProducts finishedProducts)
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            Packaging = new EFRepository<Packaging>(new ApplicationDbContext());
            sklad.InFinishedProduct(finishedProducts);
            finishedProducts.Name = FinishedProducts.FindById(finishedProducts.Id).Name;
            finishedProducts.PackagingId = FinishedProducts.FindById(finishedProducts.Id).PackagingId;
            return Json(finishedProducts, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FinishedProductJson()
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            FinishedProducts_FinishedGoodsStatistics = new EFRepository<FinishedProducts_FinishedGoodsStatistics>(new ApplicationDbContext());
            FinishedGoodsStatistics = new EFRepository<FinishedGoodsStatistics>(new ApplicationDbContext());
            var json = from js in FinishedGoodsStatistics.Get()
                       orderby js.Id descending
                       where js.Date.Value.Day == DateTime.Now.Day && js.Date.Value.Month == DateTime.Now.Month && js.Date.Value.Year == DateTime.Now.Year && FinishedGoodsStatistics.Get().Count() != 0
                       select new
                       {
                           Name = FinishedProducts.FindById(FinishedProducts_FinishedGoodsStatistics.Get(x => x.FinishedGoodsStatisticsId == js.Id).First().FinishedProductsId.Value).Name,
                           Count = js.Count == 0 ? 1 : js.Count,
                           Time = js.Date.Value.ToShortTimeString()
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Condiments()
        {
            ViewBag.Condiments = new SelectList(sklad.CondimentsViewList(), "Id", "Name");
            return View();
        }
        [HttpGet]
        public string ShowConSyr(int id)
        {
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            var syr = FinishedProducts.FindById(id);
            var str = "20";
            return str;
        }
        [HttpGet]
        public string ShowConPack(int id)
        {
            Packaging = new EFRepository<Packaging>(new ApplicationDbContext());
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            var syr = FinishedProducts.FindById(id);
            var Syr = Packaging.FindById(syr.PackagingId.Value);
            var str = string.Format(Syr.Name + " " + Syr.Count);
            return str;
        }
        public JsonResult CondimentsJson()
        {
            Condimentss = new EFRepository<Condiments>(new ApplicationDbContext());
            CondimentStatic = new EFRepository<CondimentStatic>(new ApplicationDbContext());
            Condiments_Static = new EFRepository<Condiments_Static>(new ApplicationDbContext());
            var json = from js in Condimentss.Get()
                       select new
                       {
                           Name = js.Name,
                           Count = js.Count
                       };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddCondiments()
        {
            ViewBag.sklad = new SelectList(sklad.GildsViewList(), "Id", "Name");
            ViewBag.Pack = new SelectList(sklad.PackagingList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddCondiments(Condiments condiments)
        {
            await sklad.AddCondiments(condiments);
            return Json(condiments, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> InCondiments(Condiments condiments)
        {
            Condimentss = new EFRepository<Condiments>(new ApplicationDbContext());
            condiments.Name = Condimentss.FindById(condiments.Id).Name;
            await sklad.InCondiments(condiments);

            return Json(condiments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CondimentsView()
        {
            return View(sklad.CondimentsViewList());
        }
        public ActionResult AddAll()
        {
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            ViewBag.gildSyr = new SelectList(Gild.Get(x => x.ProductId != null).ToList(), "Id", "Name");
            ViewBag.gildSpec = new SelectList(Gild.Get(x => x.CondimentsId != null).ToList(), "Id", "Name");
            ViewBag.Pack = new SelectList(sklad.PackagingList(), "Id", "Name");
            return View();
        }
        public JsonResult bar_stacked_data()
        {
            var db = new ApplicationDbContext();
            //int inc = 0;
            //var jsonresult = from ord in db.Orders.ToList()
            //                 group ord by ord.Date.Year into g
            //                 select new
            //                 {
            //                     label = g.Key,
            //                     data = from prc in g group prc by prc.Date.Month into p select p.Select(pr => pr.Price).Sum(),
            //                     backgroundColor = Charts.BackgroundColor(inc++),
            //                     borderColor = "transparent",
            //                     pointBackgroundColor = "#FFF",
            //                     pointBorderColor = "#FF5722",
            //                     pointBorderWidth = 2,
            //                     pointHoverBorderWidth = 2,
            //                     pointRadius = 4
            //                 };
            //var bar = from skd in db.Products.ToList()
            //          group skd by skd.Name into g
            //          select new
            //          {
            //              label = g.Key,
            //              data = from count in g group count by count.Date.Value.Month into p select p.Select(c=>c.Count).Sum(),
            //              backgroundColor = Charts.BackgroundColor(inc++),
            //              hoverBackgroundColor = "rgba(100,255,218,.8)",
            //              borderColor = "transparent"
            //          };

            return Json(/*bar,*/ JsonRequestBehavior.AllowGet);
        }


        public ActionResult Chart()
        {
            UserOnline.online();
            return View();
        }
        public JsonResult SyrView()
        {
            product = new EFRepository<Product>(new ApplicationDbContext());
            var json = from js in product.Get()
                       where js.Count <= 100
                       select new
                       {
                           Name = js.Name,
                           Count = js.Count
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SpecView()
        {
            Condimentss = new EFRepository<Condiments>(new ApplicationDbContext());
            var json = from js in Condimentss.Get()
                       where js.Count <= 1
                       select new
                       {
                           Name = js.Name,
                           Count = js.Count
                       };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FinishView()
        {
            Packaging = new EFRepository<Packaging>(new ApplicationDbContext());
            var json = from js in Packaging.Get()
                       where js.Count <= 50
                       select new
                       {
                           Name = js.Name,
                           Count = js.Count
                       };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FinishTable()
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            var json = from js in FinishedProducts.Get()
                       orderby js.Id
                       select new
                       {
                           Id = js.Id,
                           Name = js.Name,
                           Count = js.Count
                       };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteFinish(int id)
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            UserOnline.online();
            try
            {
                return View(FinishedProducts.FindById(id));
            }
            catch (Exception)
            {

                return HttpNotFound();
            }


        }
        [HttpPost, ActionName("DeleteFinish")]
        public ActionResult DeleteFinishConfirmed(int id)
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            FinishedProducts_FinishedGoodsStatistics = new EFRepository<FinishedProducts_FinishedGoodsStatistics>(new ApplicationDbContext());
            FinishedGoodsStatistics = new EFRepository<FinishedGoodsStatistics>(new ApplicationDbContext());
            finArray = new EFRepository<FinishGildArray>(new ApplicationDbContext());
            var finish = FinishedProducts.FindById(id);
            var fin_st = FinishedProducts_FinishedGoodsStatistics.Get(x=>x.FinishedProductsId == finish.Id);
            int ids = 0;
            foreach (var item in fin_st)
            {
                ids = item.FinishedGoodsStatisticsId.Value;
                FinishedProducts_FinishedGoodsStatistics.Remove(item);
                FinishedGoodsStatistics.Remove(FinishedGoodsStatistics.FindById(ids));
            }
            var arr = finArray.Get(x=>x.FinishId == finish.Id);
            foreach (var item in arr)
            {
                finArray.Remove(item);
            }
            
            FinishedProducts.Remove(finish);
            return RedirectToAction("AddAll");
        }

    }
}