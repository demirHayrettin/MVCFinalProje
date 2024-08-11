using Microsoft.Identity.Client;
using MVCFinalProje.Infrastructure.AppContext;
using MVCFinalProje.Infrastructure.DataAccess.EntityFramework;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.PublisherRepository
{
    internal class PublisherRepository :EFBaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context): base(context) { }
       
    }
}
