using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using MVCFİnalProje.Domain.Core.BaseEntitiyConfigurations;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Configurations
{
    public class BookConfugiration : AuditableEntityConfugiration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {

            builder.Property(b => b.Name).IsRequired().HasMaxLength(128);
            builder.Property(b => b.PublishDate).IsRequired();
            base.Configure(builder);
        }
    }
}
