using MVCFinalProje.Infrastructure.AppContext;
using MVCFinalProje.Infrastructure.DataAccess.EntityFramework;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.CustomerRepository
{
    public class CustomerRepository : EFBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context): base(context) { }
    }
}
