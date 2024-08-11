using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFinalProje.Business.Services.CustomerServices;
using MVCFinalProje.UI.Areas.Admin.Models.CustomerVMs;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    
    public class CustomerController : AdminBaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {

            var result = await _customerService.GetAllAsync();
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<List<AdminCustomerListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCustomerCreateVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _customerService.AddAsync(model.Adapt<CustomerCreateDTO>());
            if(!result.IsSucces)
            {
                ErrorNotfy(result.Message); 
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _customerService.GetByIdAsync(id);
            if (!result.IsSucces)
            {
                return NotFound();
            }
            var vm = result.Data.Adapt<AdminCustomerUpdateVM>();
            // Debugging purpose: Check the content of vm
            if (vm == null)
            {
                return NotFound(); // Or another appropriate action if view model is null
            }

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminCustomerUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _customerService.UpdateAsync(model.Adapt<CustomerUpdateDTO>());
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _customerService.DeleteAsync(id);
            if (!result.IsSucces)
            {
                // Hata mesajını kullanıcıya ilet
                ErrorNotfy(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }



    }
}
