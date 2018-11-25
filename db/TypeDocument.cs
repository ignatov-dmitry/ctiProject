using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class TypeDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Document> documets { get; set; }
        public TypeDocument()
        {
            documets = new List<Document>();
        }
    }
}
