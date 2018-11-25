using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Condiments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Count { get; set; }
        public virtual ICollection<Condiments_Static> Condiments_Statics { get; set; }
        public virtual ICollection<Gild> Gild { get; set; }
        public Condiments()
        {
            Condiments_Statics = new List<Condiments_Static>();
            Gild = new List<Gild>();
        }
    }
}
