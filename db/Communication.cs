using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Communication
    {
        public int Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ManagerId { get; set; }
    }
}
