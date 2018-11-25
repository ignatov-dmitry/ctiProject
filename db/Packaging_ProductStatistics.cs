using System.ComponentModel.DataAnnotations.Schema;

namespace db
{
    public class Packaging_ProductStatistics
    {
        public int Id { get; set; }
        public int? PackagingId { get; set; }
        [ForeignKey("PackagingId")]
        public virtual Packaging Packaging { get; set; }
        public int? ProductStatisticsId { get; set; }
        [ForeignKey("ProductStatisticsId")]
        public virtual ProductStatistics ProductStatistics { get; set; }
    }
}