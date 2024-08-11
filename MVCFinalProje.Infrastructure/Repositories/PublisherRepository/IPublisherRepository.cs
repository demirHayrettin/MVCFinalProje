using MVCFinalProje.Infrastructure.DataAccess.Interfaces;
using MVCFinalProje.Infrastructure.Repositories.AuthorRepositories;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.PublisherRepository
{
    public interface IPublisherRepository: IAsyncRepository, IAsyncFindable<Publisher>, IAsyncInsertable<Publisher>, IAsyncQueryableRepository<Publisher>, IAsyncUbdatableRepository<Publisher>, IAsyncDeletableRepository<Publisher>, IAsyncOrderableRepository<Publisher>
    {
    }
}
