using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace db
{
    //Сырье
   public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Count { get; set; }
        public virtual ICollection<Raw_Product> Raw_Prod { get; set; }
        public virtual ICollection<Gild> Gild { get; set; }
        public Product()
        {
            Gild = new List<Gild>();
            Raw_Prod = new List<Raw_Product>();
        }
    }
}
