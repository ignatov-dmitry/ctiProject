using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Logics.Hubs;

namespace Logics
{
    [Authorize]
    public static class UserOnline 
    {
        public static ChatModel chatModel = new ChatModel();
        static IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        public static void online()
        {
            if (chatModel.Users.FirstOrDefault(u => u.Name == HttpContext.Current.User.Identity.Name) != null)
            {

            }
            else
            {
                chatModel.Users.Add(new ChatUser()
                {
                    Id = Guid.Parse(HttpContext.Current.User.Identity.GetUserId()),
                    Name = HttpContext.Current.User.Identity.Name,
                    LoginTime = DateTime.Now,
                    LastPing = DateTime.Now
                });
                context.Clients.All.UserConnect(HttpContext.Current.User.Identity.Name);
                context.Clients.All.RefreshUsersOnline();
            }
            

            ChatUser currentUser = chatModel.Users.FirstOrDefault(u => u.Name == HttpContext.Current.User.Identity.Name);


            currentUser.LastPing = DateTime.Now;


            List<ChatUser> toRemove = new List<ChatUser>();
            foreach (ChatUser usr in chatModel.Users)
            {
                TimeSpan span = DateTime.Now - usr.LastPing;
                if (span.TotalSeconds > 100)
                    toRemove.Add(usr);
            }
            foreach (ChatUser u in toRemove)
            {
                chatModel.Users.Remove(u);

            }
        }

    }
}
