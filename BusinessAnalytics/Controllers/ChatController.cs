using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessAnalytics.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using db;
using Logics.Hubs;
using Microsoft.AspNet.SignalR;
using System.Web.Http;
using Logics;

namespace BusinessAnalytics.Controllers
{
    [Microsoft.AspNet.SignalR.Authorize]
    public class ChatController : Controller
    {
        static TaskObj obj = new TaskObj();
        static ChatMessage chatMes = new ChatMessage();
        static ChatModel chat = new ChatModel();
        static void readMessage(Guid id, Guid getId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var mess = context.ChatMessages.Where(c => c.Read == false && c.UserSetId == id && c.UserGetId == getId).AsEnumerable().Select(a => { a.Read = true; return a; }).ToList();
            foreach (var mes in context.ChatMessages)
            {
                context.Entry(mes).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public ActionResult Index(bool? logOn, bool? logOff, string chatMessage)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home");
            }
                UserOnline.online();
                var db = new ApplicationDbContext();
                var online = from onl in UserOnline.chatModel.Users.ToList()
                             where onl.Name != User.Identity.Name
                             select onl;


            ViewBag.online = online.ToList();
            ViewBag.newUsers = db.Users.ToList();

            try
                {
                    if (!Request.IsAjaxRequest())
                    {
                        return View(UserOnline.chatModel);
                    }

                    else if (logOn != null && (bool)logOn)
                    {

                        return PartialView("Index", UserOnline.chatModel);
                    }

                    else if (logOff != null && (bool)logOff)
                    {
                        return PartialView("Index", UserOnline.chatModel);
                    }
                    else
                    {
                        return PartialView("History", UserOnline.chatModel);
                    }
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 500;
                    return Content(ex.Message);
                }
           
        }
        public ActionResult PrivateChat(Guid? Id, string chatMessage ="", bool logOn = true, bool enter = true)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home");
            }
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            readMessage(Guid.Parse(Id.ToString()), Guid.Parse(User.Identity.GetUserId()));
            var db = new ApplicationDbContext();
            var us = UserOnline.chatModel;
            var query = from messe in db.ChatMessages.ToList()
                        where ((messe.UserSetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserGetId == Id)) || ((messe.UserGetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserSetId == Id))
                        select new ChatMessage()
                        {
                            Id = messe.Id,
                            Date = messe.Date,
                            Text = messe.Text,
                            UserGet = messe.UserGet,
                            UserGetId = messe.UserGetId,
                            UserSet = messe.UserSet,
                            UserSetId = messe.UserSetId
                        };
            var online = from onl in UserOnline.chatModel.Users.ToList()
                         where onl.Name != User.Identity.Name
                         select onl;

            
            
            ViewBag.online = online.ToList();
            ViewBag.newUsers = db.Users.ToList();
            ViewBag.query = query;
            ViewBag.Users = us.Users;
            

            if (logOn == false)
            {
                return View("PrivateChat", chatMes);
            }
            else
            {
                if (!string.IsNullOrEmpty(chatMessage))
                {

                    if (online.Count() != 0)
                        chatMes.Read = true;
                    else
                        chatMes.Read = false;
                    chatMes.Date = DateTime.Now;
                    chatMes.Text = chatMessage;
                    chatMes.UserSetId = Guid.Parse(User.Identity.GetUserId());
                    chatMes.UserGetId = Id;
                    db.ChatMessages.Add(chatMes);
                    db.SaveChanges();
                    query = from messe in db.ChatMessages.ToList()
                            where ((messe.UserSetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserGetId == Id)) || ((messe.UserGetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserSetId == Id))
                            select new ChatMessage()
                                {
                                    Id = messe.Id,
                                    Date = messe.Date,
                                    Text = messe.Text,
                                    UserGet = messe.UserGet,
                                    UserGetId = messe.UserGetId,
                                    UserSet = messe.UserSet,
                                    UserSetId = messe.UserSetId
                                };
                    context.Clients.User(obj.ViewUserName(Guid.Parse(Id.ToString())).UserName).UserSend(User.Identity.Name);
                    context.Clients.All.updt();
                    ViewBag.query = query;
                    return PartialView("PrivateChatHistory", chatMes);
                }
                else if(enter)
                {
                    enter = false;
                    if (!Request.IsAjaxRequest())
                    {
                        return View(chatMes);
                    }
                    return PartialView("PrivateChat", chatMes);

                }
                else
                {
                    if (!Request.IsAjaxRequest())
                    {
                        return View(chatMes);
                    }
                    return PartialView("PrivateChatHistory", chatMes);
                }
            }

            
        }


        public ActionResult MiniChat(Guid? Id, string chatMessage, bool logOn = true, bool enter = false)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home");
            }
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            if(Id != null)
            readMessage(Guid.Parse(Id.ToString()), Guid.Parse(User.Identity.GetUserId()));
            var db = new ApplicationDbContext();
            var us = UserOnline.chatModel;
            var query = from messe in db.ChatMessages.ToList()
                        where ((messe.UserSetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserGetId == Id)) || ((messe.UserGetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserSetId == Id))
                        select new ChatMessage()
                        {
                            Id = messe.Id,
                            Date = messe.Date,
                            Text = messe.Text,
                            UserGet = messe.UserGet,
                            UserGetId = messe.UserGetId,
                            UserSet = messe.UserSet,
                            UserSetId = messe.UserSetId
                        };
            var online = from onl in UserOnline.chatModel.Users.ToList()
                         where onl.Name != User.Identity.Name
                         select onl;


            var user = Guid.Parse(User.Identity.GetUserId());
            ViewBag.online = online.ToList();
            ViewBag.newUsers = db.Users.ToList();
            ViewBag.query = query;
            ViewBag.Users = us.Users;


            if (logOn == false)
            {
                return PartialView("MiniChat");
            }
            else
            {
                if (!string.IsNullOrEmpty(chatMessage))
                {

                    if (online.Count() != 0)
                        chatMes.Read = true;
                    else
                        chatMes.Read = false;
                    chatMes.Date = DateTime.Now;
                    chatMes.Text = chatMessage;
                    chatMes.UserSetId = Guid.Parse(User.Identity.GetUserId());
                    chatMes.UserGetId = Id;
                    db.ChatMessages.Add(chatMes);
                    db.SaveChanges();
                    query = from messe in db.ChatMessages.ToList()
                            where ((messe.UserSetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserGetId == Id)) || ((messe.UserGetId == Guid.Parse(User.Identity.GetUserId())) && (messe.UserSetId == Id))
                            select new ChatMessage()
                            {
                                Id = messe.Id,
                                Date = messe.Date,
                                Text = messe.Text,
                                UserGet = messe.UserGet,
                                UserGetId = messe.UserGetId,
                                UserSet = messe.UserSet,
                                UserSetId = messe.UserSetId
                            };
                    context.Clients.User(obj.ViewUserName(Guid.Parse(Id.ToString())).UserName).UserSend(User.Identity.Name);
                    context.Clients.All.updt();
                    ViewBag.query = query;
                    return PartialView("MiniChat");
                }
                else if (enter)
                {
                    enter = false;
                    if (!Request.IsAjaxRequest())
                    {
                        return View(chatMes);
                    }
                    return PartialView("MiniChat");

                }
                else
                {
                    if (!Request.IsAjaxRequest())
                    {
                        return View(chatMes);
                    }
                    return PartialView("MiniChat");
                }
            }


        }



    }
}