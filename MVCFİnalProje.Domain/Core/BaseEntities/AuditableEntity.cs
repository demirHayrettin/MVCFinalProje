﻿using MVCFİnalProje.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFİnalProje.Domain.Core.BaseEntities
{
    public class AuditableEntity : BaseEntity, IDeletableEntity
    {
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
