using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class ProductStatistics
    {
        public int Id { get; set; }
        public virtual ICollection<Packaging_ProductStatistics> Packaging_ProductStatistics { get; set; }
        public DateTime? Date { get; set; }
        public double Count { get; set; }
        public virtual ICollection<FinishedProducts> FinishedProducts { get; set; }
        public virtual ICollection<FinishedGoodsStatistics> FinishedGoodsStatistics { get; set; }
        public ProductStatistics()
        {
            Packaging_ProductStatistics = new List<Packaging_ProductStatistics>();
            FinishedProducts = new List<FinishedProducts>();
            FinishedGoodsStatistics = new List<FinishedGoodsStatistics>();
        }
    }
}
