using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class FinishGildArray
    {
        public int Id { get; set; }
        public int? GildId { get; set; }
        public int? FinishId { get; set; }
        [ForeignKey("FinishId")]
        public virtual FinishedProducts FinishedProducts { get; set; }
        public int? FinishStaticId { get; set; }
        [ForeignKey("FinishStaticId")]
        public virtual FinishedGoodsStatistics FinishedGoodsStatistics { get; set; }
        public double? CountSyr { get; set; }
        public double? CountSpec { get; set; }
    }
}
