using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Claim
    {
        public int Id { get; set; }
        public Guid IdClient { get; set; }
        public Guid? IdManager { get; set; }
        public bool? PrinClient { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? PrinManager { get; set; }
        public DateTime? OtprAuto { get; set; }
        public double ObshSum { get; set; }
        public virtual ICollection<ClaimArray> ClaimArray { get; set; }
        public Claim() {
            ClaimArray = new List<ClaimArray>();
        }
    }
}
