using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.DTOs.BookDTOs;
using MVCFinalProje.Business.Services.AuthorServices;
using MVCFinalProje.Business.Services.BookServices;
using MVCFinalProje.Business.Services.PublisherServices;
using MVCFinalProje.UI.Areas.Admin.Models.AuthorVMs;
using MVCFinalProje.UI.Areas.Admin.Models.BookVMs;
using MVCFİnalProje.Domain.Entities;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{

    public class BookController : AdminBaseController
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllAsync();
            var adminBookListVM = result.Data.Adapt<List<AdminBookListVM>>();
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                //await Console.Out.WriteAsync(result.Message);
                return View(result.Data.Adapt<List<AdminBookListVM>>());
            }
            SuccessNotfy(result.Message);
            return View(adminBookListVM);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Create()
        {
            AdminBookCreateVM vm = new AdminBookCreateVM();
            vm.Authors = await GetAuthors();
            vm.Publishers = await GetPublishers();

            return View(vm);
        }


        private async Task<SelectList> GetPublishers(Guid? publisherId = null)
        {
            var publishers = (await _publisherService.GetAllAsync()).Data;
            return new SelectList(publishers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (publisherId != null ? publisherId.Value : publisherId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }


        public async Task<SelectList> GetAuthors(Guid? authorId = null)
        {
            var authors = (await _authorService.GetAllAsync()).Data;
            return new SelectList(authors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (authorId != null ? authorId.Value : authorId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }


        [HttpPost]
        public async Task<IActionResult> Create(AdminBookCreateVM model)
        {
            var result = await _bookService.AddAsync(model.Adapt<BookCreateDTO>());
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _bookService.GetByIdAsync(id);
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);

            var vm = result.Data.Adapt <AdminBookUpdateVM>();

            vm.Authors = await GetAuthors(vm.AuthorId);
            vm.Publishers = await GetPublishers(vm.PublisherId);
           

            return View(vm);
        }

        [HttpPost]        
        public async Task<IActionResult> Update(AdminBookUpdateVM model)
        {

            if (!ModelState.IsValid)
            {
                model.Authors = await GetAuthors(model.AuthorId);
                model.Publishers = await GetPublishers(model.PublisherId);
                return View(model);
            }

            var result = await _bookService.UpdateAsync(model.Adapt<BookUpdateDTO>());
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                model.Authors = await GetAuthors(model.AuthorId);
                model.Publishers = await GetPublishers(model.PublisherId);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }

    }
}
