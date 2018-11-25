using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
   public class UserModel
    {
        public int Id { get; set; }
        public Guid AspNetUserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Company { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastPing { get; set; }
    }
}
