using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Gild_GildStatic
    {
        public int Id { get; set; }
        public int? GildId { get; set; }
        [ForeignKey("GildId")]
        public virtual Gild Gild { get; set; }
        public int? GildStaticId { get; set; }
        [ForeignKey("GildStaticId")]
        public virtual GildStatic GildStatics { get; set; }
    }
}
