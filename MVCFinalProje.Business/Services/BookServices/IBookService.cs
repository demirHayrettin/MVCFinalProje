using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.DTOs.BookDTOs;
using MVCFİnalProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.BookServices
{
    public interface IBookService
    {
        Task<IResult> AddAsync(BookCreateDTO bookCreateDTO);
        Task<IDataResult<List<BookListDTO>>> GetAllAsync();

        Task<IResult> DeleteAsync(Guid id);

        Task<IDataResult<BookDTO>> GetByIdAsync(Guid id);

        Task<IResult> UpdateAsync(BookUpdateDTO bookUpdateDTO);
    }
}
