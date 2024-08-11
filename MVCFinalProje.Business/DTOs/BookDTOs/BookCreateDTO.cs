﻿using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.DTOs.BookDTOs
{
    public class BookCreateDTO
    {
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }               
        public Guid AuthorId { get; set; }       
        public Guid PublisherId { get; set; }
    }
}
