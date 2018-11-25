using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    //Готовая продукция
    public class FinishedProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int CountPack { get; set; }
        public decimal PriceIn { get; set; }
        public decimal PriceOut { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<FinishGildArray> FinishGildArray { get; set; }
        public int? PackagingId { get; set; }
        [ForeignKey("PackagingId")]
        public virtual Packaging Packaging { get; set; }
        public virtual ICollection<FinishedProducts_FinishedGoodsStatistics> FinishedProducts_FinishedGoodsStatistics { get; set; }
        public virtual ICollection<ClaimArray> ClaimArrays { get; set; }
        public FinishedProducts()
        {
            FinishedProducts_FinishedGoodsStatistics = new List<FinishedProducts_FinishedGoodsStatistics>();
            FinishGildArray = new List<FinishGildArray>();
            ClaimArrays = new List<ClaimArray>();
        }
    }
}
