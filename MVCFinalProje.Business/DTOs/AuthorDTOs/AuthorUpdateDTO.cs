using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.DTOs.AuthorDTOs
{
    public class AuthorUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
      
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
    }
}
