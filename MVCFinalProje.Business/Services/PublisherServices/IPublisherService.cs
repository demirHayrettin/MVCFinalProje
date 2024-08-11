using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFİnalProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.PublisherServices
{
    public interface IPublisherService
    {
        Task<IResult> AddAsync(PublisherCreateDTO publisherCreateDTO);
        Task<IDataResult<List<PublisherListDTO>>> GetAllAsync();

        Task<IResult> DeleteAsync(Guid id);

        Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id);

        Task<IResult> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO);
    }
}
