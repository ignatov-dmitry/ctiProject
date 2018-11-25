using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class EditModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string FIO { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
        public byte[] Image { get; set; }
    }
}
