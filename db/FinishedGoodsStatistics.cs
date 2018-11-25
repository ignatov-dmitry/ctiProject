using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    //Статистика о готов продукции
    public class FinishedGoodsStatistics
    {
        public int Id { get; set; }
        public virtual ICollection<FinishedProducts_FinishedGoodsStatistics> FinishedProducts_FinishedGoodsStatistics { get; set; }
        public DateTime? Date { get; set; }
        public int Count { get; set; }
        public decimal PriceIn { get; set; }
        public decimal PriceOut { get; set; }
        public virtual ICollection<FinishGildArray> FinishGildArray { get; set; }
        public int? PackagingId { get; set; }
        public double CountProd { get; set; }
        public double CountSpec { get; set; }
        public int CountPack { get; set; }
        [ForeignKey("PackagingId")]
        public virtual Packaging Packaging { get; set; }
        public FinishedGoodsStatistics()
        {
            FinishedProducts_FinishedGoodsStatistics = new List<FinishedProducts_FinishedGoodsStatistics>();
            FinishGildArray = new List<FinishGildArray>();
        }
    }
}
