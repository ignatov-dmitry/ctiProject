using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class Auto
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string NumberAuto { get; set; }
        public double ObmDvig { get; set; }
        public string TypeTopl { get; set; }
        public double ObmKuz { get; set; }
        public int Year { get; set; }
        public bool Status { get; set; }
    }
}
