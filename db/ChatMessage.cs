using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public Guid? UserSetId { get; set; }
        public Guid? UserGetId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool? Read { get; set; }
        public ApplicationUser UserSet { get; set; }
        public ApplicationUser UserGet { get; set; }
    }
}
