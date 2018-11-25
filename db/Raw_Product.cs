using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Raw_Product
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int? RawId { get; set; }
        [ForeignKey("RawId")]
        public RawMaterialStatistics RawMaterial { get; set; }

    }
}
