using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    //Статистика сырья
    public class RawMaterialStatistics
    {
        public int Id { get; set; }
        
        public virtual ICollection<Raw_Product> Raw_Product { get; set; }
        public DateTime? Date { get; set; }
        public double Count { get; set; }
        public RawMaterialStatistics()
        {
            Raw_Product = new List<Raw_Product>();
        }
    }
}
