using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class GildStatic
    {
        public int Id { get; set; }
        public double CountGild { get; set; }
        public DateTime Date { get; set; }
        public double CountCondiments { get; set; }
        public double CountProduct { get; set; }
        public virtual ICollection<Gild_GildStatic> Gild_GildStatics { get; set; }
        public GildStatic()
        {
            Gild_GildStatics = new List<Gild_GildStatic>();
        }
    }
}
