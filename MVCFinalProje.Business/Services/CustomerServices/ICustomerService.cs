using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFİnalProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<IResult> AddAsync(CustomerCreateDTO customerCreateDTO);
        Task<IDataResult<List<CustomerListDTO>>> GetAllAsync();
        Task<IResult> UpdateAsync(CustomerUpdateDTO customerUpdateDTO);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<CustomerDTO>> GetByIdAsync(Guid id);
    }
}
