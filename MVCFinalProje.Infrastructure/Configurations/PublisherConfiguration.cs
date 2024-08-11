using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCFİnalProje.Domain.Core.BaseEntitiyConfigurations;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Configurations
{
    public class PublisherConfiguration : AuditableEntityConfugiration<Publisher>
    {

        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(128);
            base.Configure(builder);
        }
    }
}
