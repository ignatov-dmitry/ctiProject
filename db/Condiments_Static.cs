using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Condiments_Static
    {
        public int Id { get; set; }
        public int? CondimentsId { get; set; }
        [ForeignKey("CondimentsId")]
        public virtual Condiments Condiments { get; set; }
        public int? CondimentStaticId { get; set; }
        [ForeignKey("CondimentStaticId")]
        public virtual CondimentStatic CondimentStatic { get; set; }
    }
}
