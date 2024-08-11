﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCFİnalProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFİnalProje.Domain.Core.BaseEntitiyConfigurations
{
    public class AuditableEntityConfugiration<TEntity>:BaseEntityConfugiration<TEntity> where TEntity : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.Property(x => x.DeletedBy).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
            
            base.Configure(builder);
        }
    }
}
