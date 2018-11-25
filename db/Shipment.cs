using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Shipment
    {
        public int Id { get; set; }
        public ICollection<Shipment_Array> shipment_Arrays { get; set; }
        public double MassOstAuto { get; set; }
        public DateTime? PrinVyhOtpr { get; set; }
        public bool? Status { get; set; }
        public int? AutoId { get; set; }
        
        public Shipment()
        {
            shipment_Arrays = new List<Shipment_Array>();
        }
    }
}
