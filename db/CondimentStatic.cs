using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class CondimentStatic
    {
        public int Id { get; set; }
        public double Count { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Condiments_Static> Condiments_Static { get; set; }
        public CondimentStatic()
        {
            Condiments_Static = new List<Condiments_Static>();
        }
    }
}
