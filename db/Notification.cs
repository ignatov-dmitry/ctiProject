using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Notification
    {
        public int id { get; set; }
        public Guid AspNetUserId { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Url { get; set; }
        public Notification()
        {

        }
    }
}
