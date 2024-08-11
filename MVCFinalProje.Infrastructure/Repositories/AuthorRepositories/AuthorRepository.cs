using Microsoft.EntityFrameworkCore;
using MVCFinalProje.Infrastructure.AppContext;
using MVCFinalProje.Infrastructure.DataAccess.EntityFramework;
using MVCFİnalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.AuthorRepositories
{
    public class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
