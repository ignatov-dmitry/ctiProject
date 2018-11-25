using System.ComponentModel.DataAnnotations.Schema;

namespace db
{
    public class FinishedProducts_FinishedGoodsStatistics
    {
        public int Id { get; set; }
        public int? FinishedProductsId { get; set; }
        [ForeignKey("FinishedProductsId")]
        public virtual FinishedProducts FinishedProducts { get; set; }
        public int? FinishedGoodsStatisticsId { get; set; }
        [ForeignKey("FinishedGoodsStatisticsId")]
        public virtual FinishedGoodsStatistics FinishedGoodsStatistics { get; set; }
    }
}