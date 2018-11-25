using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    
    public class Gild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int? CondimentsId { get; set; }
        [ForeignKey("CondimentsId")]
        public virtual Condiments Condiments { get; set; }
        public bool State { get; set; }
        public virtual ICollection<Gild_GildStatic> Gild_GildStatics { get; set; }
        public Gild()
        {
            Gild_GildStatics = new List<Gild_GildStatic>();
        }
    }
}
