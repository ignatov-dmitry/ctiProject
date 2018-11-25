using System.ComponentModel.DataAnnotations.Schema;

namespace db
{
    public class ClaimArray
    {
        public int Id { get; set; }
        public int? FinishProductId { get; set; }
        [ForeignKey("FinishProductId")]
        public virtual FinishedProducts FinishedProducts { get; set; }
        public int Count { get; set; }
    }
}