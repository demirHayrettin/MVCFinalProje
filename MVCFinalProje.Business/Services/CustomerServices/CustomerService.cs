using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFinalProje.Business.Services.AccountServices;
using MVCFinalProje.Infrastructure.Repositories.CustomerRepository;
using MVCFİnalProje.Domain.Entities;
using MVCFİnalProje.Domain.Enums;
using MVCFİnalProje.Domain.Utilities.Concretes;
using MVCFİnalProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCFinalProje.Business.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerService(IAccountService accountService, ICustomerRepository customerRepository, UserManager<IdentityUser> userManager)
        {
            _accountService = accountService;
            _customerRepository = customerRepository;
            _userManager = userManager;
        }

        public async Task<IResult> AddAsync(CustomerCreateDTO customerCreateDTO)
        {
            if (await _accountService.AnyAsync(x => x.Email == customerCreateDTO.Email))
            {
                return new ErrorResult("Email adresi kullanılıyor");
            }
            IdentityUser user = new IdentityUser()
            {
                Email = customerCreateDTO.Email,
                NormalizedEmail = customerCreateDTO.Email.ToUpperInvariant(),
                UserName = customerCreateDTO.Email,
                NormalizedUserName = customerCreateDTO.Email.ToUpperInvariant(),
                EmailConfirmed = true
            };

            Result result = new Result();
            var strategy = await _customerRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var identityResult = await _accountService.CreateUserAsync(user, Roles.Customer);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorResult(identityResult.ToString());
                        transactionScope.Rollback();
                        return;
                    }
                    var newCustomer = customerCreateDTO.Adapt<Customer>();
                    newCustomer.IdentityId = user.Id;
                    await _customerRepository.AddAsync(newCustomer);
                    await _customerRepository.SaveChangeAsync();
                    result = new SuccesResult("Müşteri kaydı başarılı");
                    transactionScope.Commit();

                }
                catch (Exception ex)
                {

                    result = new ErrorResult("Hata :" + ex.Message);
                    transactionScope.Rollback();
                }
                finally
                {
                    transactionScope.Dispose();
                }
            });
            return result;

        }

        public async Task<IResult> UpdateAsync(CustomerUpdateDTO customerUpdateDTO)
        {
            var updatingCustomer = await _customerRepository.GetByIdAsync(customerUpdateDTO.Id);
            if (updatingCustomer == null)
            {
                return new ErrorResult("Müşteri bulunamadı");
            }

            IdentityUser user;
            try
            {
                user = await _userManager.FindByIdAsync(updatingCustomer.IdentityId);
                if (user == null)
                {
                    return new ErrorResult("Kullanıcı bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }

            user.Email = customerUpdateDTO.Email;
            user.NormalizedEmail = customerUpdateDTO.Email.ToUpperInvariant();
            user.UserName = customerUpdateDTO.Email;
            user.NormalizedUserName = customerUpdateDTO.Email.ToUpperInvariant();

            Result result = new Result();
            var strategy = await _customerRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var identityResult = await _userManager.UpdateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorResult(identityResult.Errors.FirstOrDefault()?.Description ?? "Kullanıcı güncelleme başarısız");
                        transactionScope.Rollback();
                        return;
                    }

                    updatingCustomer = customerUpdateDTO.Adapt(updatingCustomer);
                    await _customerRepository.UpdateAsync(updatingCustomer);
                    await _customerRepository.SaveChangeAsync();
                    result = new SuccesResult("Müşteri güncelleme başarılı");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    result = new ErrorResult("Hata :" + ex.Message);
                    transactionScope.Rollback();
                }
                finally
                {
                    transactionScope.Dispose();
                }
            });

            return result;
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingCustomer = await _customerRepository.GetByIdAsync(id);
            if (deletingCustomer == null)
            {
                return new ErrorResult("Müşteri bulunamadı");
            }

            IdentityUser user;
            try
            {
                user = await _userManager.FindByIdAsync(deletingCustomer.IdentityId);
                if (user == null)
                {
                    return new ErrorResult("Kullanıcı bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }

            var result = new Result();
            var strategy = await _customerRepository.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var identityResult = await _userManager.DeleteAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorResult(identityResult.Errors.FirstOrDefault()?.Description ?? "Kullanıcı silme başarısız");
                        await transactionScope.RollbackAsync(); // Asenkron rollback
                        return;
                    }

                    await _customerRepository.DeleteASync(deletingCustomer); // Düzeltme: DeleteASync yerine DeleteAsync
                    await _customerRepository.SaveChangeAsync(); // Düzeltme: SaveChangeAsync yerine SaveChangesAsync
                    result = new SuccesResult("Müşteri silme başarılı");
                    await transactionScope.CommitAsync(); // Asenkron commit
                }
                catch (Exception ex)
                {
                    result = new ErrorResult("Hata :" + ex.Message);
                    await transactionScope.RollbackAsync(); // Asenkron rollback
                }
                finally
                {
                    await transactionScope.DisposeAsync(); // Asenkron dispose
                }
            });

            return result;
        }





        public async Task<IDataResult<List<CustomerListDTO>>> GetAllAsync()
        {
            var customerDTOs = (await _customerRepository.GetAllAsync()).Adapt<List<CustomerListDTO>>();
            return new SuccesDataResult<List<CustomerListDTO>>(customerDTOs, "Müşteri listeleme başarılı");
        }

        public async Task<IDataResult<CustomerDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    // customer null olduğunda Adapt çağırmak hataya yol açar
                    return new ErrorDataResult<CustomerDTO>(null, "Güncellenecek Müşteri Bulunamadı");
                }

                // Adapt çağrısını customer null değilse yap
                var customerDTO = customer.Adapt<CustomerDTO>();
                return new SuccesDataResult<CustomerDTO>(customerDTO, "Güncellenecek Müşteri Getirildi");
            }
            catch (Exception ex)
            {
                // Hata durumunda null döndürmek
                return new ErrorDataResult<CustomerDTO>(null, "Hata: " + ex.Message);
            }
        }


        //public async Task<IDataResult<CustomerDTO>> GetByIdAsync(Guid id)
        //{
        //    var customer = await _customerRepository.GetByIdAsync(id);
        //    try
        //    {
        //        if(customer == null)
        //        {
        //            return new ErrorDataResult<CustomerDTO>(customer.Adapt<CustomerDTO>(), "Güncellenecek Müşteri Bulunamadı");
        //        }
        //        return new SuccesDataResult<CustomerDTO>(customer.Adapt<CustomerDTO>(), "Güncellenecek Müşteri Getirildi");
        //    }
        //    catch (Exception ex)
        //    {

        //        return new ErrorDataResult<CustomerDTO>(customer.Adapt<CustomerDTO>(), "Hata :" + ex.Message);
        //    }
        //}
    }

}
