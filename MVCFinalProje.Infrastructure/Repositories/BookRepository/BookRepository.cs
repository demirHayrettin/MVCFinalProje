using MVCFinalProje.Infrastructure.AppContext;
using MVCFinalProje.Infrastructure.DataAccess.EntityFramework;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.BookRepository
{
    public class BookRepository : EFBaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }
        
    }
}
