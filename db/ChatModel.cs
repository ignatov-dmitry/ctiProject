using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db
{
    public class ChatModel
    {
        public List<ChatUser> Users;

        public ChatModel()
        {
            Users = new List<ChatUser>();
        }
    }

    public class ChatUser
    {
        public Guid Id;
        public string Name;
        public DateTime LoginTime;
        public DateTime LastPing;
    }
}