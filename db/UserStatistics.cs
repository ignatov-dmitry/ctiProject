using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class UserStatistics
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string URL { get; set; }
        public string Message { get; set; }
        public DateTime DateOfCompletion { get; set; }
    }
}
