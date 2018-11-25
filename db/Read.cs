using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Read
    {
        public Int32 Id { get; set; }
        public Guid? User { get; set; }
        public String Name { get; set; }
        public DateTime? Date { get; set; }
        public Int32 Count { get; set; }
    }
}
