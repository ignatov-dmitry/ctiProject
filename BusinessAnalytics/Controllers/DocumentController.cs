using db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Logics;
using BusinessAnalytics.Models;

namespace BusinessAnalytics.Controllers
{
    public class DocumentController : Controller
    {
        static DocumentObj obj = new DocumentObj();
        // GET: Document
        public ActionResult ViewСlaim()
        {
            UserOnline.online();
            return View();
        }
        public ActionResult ViewAct()
        {
            UserOnline.online();
            return View();
        }
        public ActionResult ViewInvoice()
        {
            UserOnline.online();
            return View();
        }
        public ActionResult AddDocument(Document doc)
        {
            UserOnline.online();
            obj.Add(doc);
            return View();
        }
    }
}