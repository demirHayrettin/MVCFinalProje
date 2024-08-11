using MVCFinalProje.Infrastructure.DataAccess.Interfaces;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.AuthorRepositories
{
    public interface IAuthorRepository: IAsyncRepository, IAsyncFindable<Author>, IAsyncInsertable<Author>, IAsyncQueryableRepository<Author>, IAsyncUbdatableRepository<Author>, IAsyncDeletableRepository<Author>
    {
    }
}
