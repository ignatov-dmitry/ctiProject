using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    //Упаковка
    public class Packaging
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Amount { get; set; }
        public virtual ICollection<Packaging_ProductStatistics> Packaging_ProductStatistics { get; set; }
        public virtual ICollection<FinishedProducts> FinishedProducts { get; set; }
        public virtual ICollection<FinishedGoodsStatistics> FinishedGoodsStatistics { get; set; }
        public Packaging()
        {
            Packaging_ProductStatistics = new List<Packaging_ProductStatistics>();
            FinishedProducts = new List<FinishedProducts>();
            FinishedGoodsStatistics = new List<FinishedGoodsStatistics>();
        }
    }
}
