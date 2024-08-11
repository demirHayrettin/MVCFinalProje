using MVCFİnalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.CustomerRepository
{
    public interface ICustomerRepository : IAsyncRepository, IAsyncFindable<Customer>, IAsyncInsertable<Customer>, IAsyncQueryableRepository<Customer>, IAsyncUbdatableRepository<Customer>, IAsyncDeletableRepository<Customer>,IAsyncTransactionRepository
    {


    }
}
