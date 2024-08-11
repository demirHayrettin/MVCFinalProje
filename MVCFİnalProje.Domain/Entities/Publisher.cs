using MVCFİnalProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFİnalProje.Domain.Entities
{
    public class Publisher : AuditableEntity
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }
    }
}
