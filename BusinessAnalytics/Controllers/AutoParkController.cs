using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using db;
using Logics;

namespace BusinessAnalytics.Controllers
{
    public class AutoParkController : Controller
    {
        private AutoParlLogic autoParlLogic;
        [Authorize(Roles = "Auto")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AutoList()
        {
            ViewBag.Title = "Автомобили";
            return View();
        }
        public JsonResult AutoListTable()
        {
            autoParlLogic = new AutoParlLogic();
            
                var json = autoParlLogic.AutoAllList().ToList();
                return Json(json, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public async Task<ViewResult> CreateAutoNew()
        {
            ViewBag.TitlePart = "Добавления нового автомобиля";
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> CreateAutoNew(Auto auto)
        {

            autoParlLogic = new AutoParlLogic();
                await autoParlLogic.CreateNewAuto(auto);
                return Json(auto, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public async Task<ViewResult> EditAutoNew(int id)
        {
            ViewBag.TitlePart = "Изминение характеристик автомобиля";
            autoParlLogic = new AutoParlLogic();
            return View(autoParlLogic.ViewOneAuto(id));
        }
        [HttpPost]
        public async Task<JsonResult> EditAutoNew(Auto auto)
        {
            autoParlLogic = new AutoParlLogic();
            await autoParlLogic.EditAutoIn(auto);
                return Json(auto, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public async Task<ViewResult> DeleteAutoNew()
        {
            ViewBag.TitlePart = "Удаления автомобиля";
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> DeleteAutoNew(Int32 autoId)
        {
            autoParlLogic = new AutoParlLogic();
            await autoParlLogic.RemoveAutoIn(autoId);
                return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DriverList()
        {
            ViewBag.Title = "Водители";
            return View();
        }
        [HttpGet]
        public JsonResult DriverListTable()
        {
            autoParlLogic = new AutoParlLogic();
            autoParlLogic = new AutoParlLogic();
                return Json(autoParlLogic.DriverAllList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ViewResult> CreateDriverNew()
        {
            ViewBag.TitlePart = "Добавления нового водителя";
            autoParlLogic = new AutoParlLogic();
            ViewBag.sel = new SelectList(autoParlLogic.AutoNoneDriverList(), "Id", "Model");
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> CreateDriverNew(Driver driver)
        {
            autoParlLogic = new AutoParlLogic();
            await autoParlLogic.CreateNewDriver(driver);
            return Json(driver, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ViewResult> EditDriverNew(int id)
        {
            ViewBag.TitlePart = "Изминение данных о водителе";
            autoParlLogic = new AutoParlLogic();
            ViewBag.sel = new SelectList(autoParlLogic.AutoNoneDriverList(), "Id", "Model");
            return View(autoParlLogic.ViewOneDriver(id));
        }
        [HttpPost]
        public async Task<JsonResult> EditDriverNew(Driver driver)
        {
            autoParlLogic = new AutoParlLogic();
                await autoParlLogic.EditDriverIn(driver);
                return Json(driver, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ViewResult> DeleteDriverNew()
        {
            ViewBag.TitlePart = "Удаления водителя";
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> DeleteDriverNew(Int32 driverId)
        {
            autoParlLogic = new AutoParlLogic();
            await autoParlLogic.RemoveDriverIn(driverId);
                return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StatusAuto()
        {
            ViewBag.Title = "Статус автомобилей";
            return View();
        }
        public ActionResult ViewPopapInfo(int Id)
        {
            return View(Id);
        }
    }
}