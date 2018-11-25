using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Shipment_Array
    {
        public int Id { get; set; }
        public double ObchMasClaim { get; set; }
        public int? ClaimId { get; set; }
        public DateTime? DatePrin { get; set; }
        public bool? Status { get; set; }
        public string NameClient { get; set; }
        public int? IdShipment { get; set; }
        [ForeignKey("IdShipment")]
        public Shipment shipment { get; set; }
    }
}
