using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.Services.AuthorServices;
using Mapster;
using MVCFinalProje.UI.Areas.Admin.Models.AuthorVMs;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    public class AuthorController : AdminBaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorService.GetAllAsync();
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                //await Console.Out.WriteAsync(result.Message);
                return View(result.Data.Adapt<List<AdminAuthorLİstVM>>());
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<List<AdminAuthorLİstVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAuthorCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var authorCreateDto = model.Adapt<AuthorCreateDTO>();
            var result = await _authorService.AddAsync(authorCreateDto);
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return View(model);
            }
            SuccessNotfy(result.Message);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _authorService.DeleteAsync(id);

            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<AdminAuthorUpdateVM>());
        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminAuthorUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _authorService.UpdateAsync(model.Adapt<AuthorUpdateDTO>());
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }
    }
}
