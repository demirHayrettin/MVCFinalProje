using Mapster;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.DTOs.BookDTOs;
using MVCFinalProje.Infrastructure.Repositories.BookRepository;
using MVCFİnalProje.Domain.Entities;
using MVCFİnalProje.Domain.Utilities.Concretes;
using MVCFİnalProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IResult> AddAsync(BookCreateDTO bookCreateDTO)
        {
            if (await _bookRepository.AnyAsync(x => x.Name.ToLower() == bookCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Kitap Sistemde Kayıtlı");
            }
            try
            {
                var newBook = bookCreateDTO.Adapt<Book>();
                await _bookRepository.AddAsync(newBook);
                await _bookRepository.SaveChangeAsync();
                return new SuccesResult("Kitap Ekleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingBook = await _bookRepository.GetByIdAsync(id);

            if (deletingBook == null)
            {
                return new ErrorResult("Silinecek Kitap Bulunumadı");
            }

            try
            {
                await _bookRepository.DeleteASync(deletingBook);
                await _bookRepository.SaveChangeAsync();
                return new SuccesResult("Kitap Silme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata :" + ex.Message);
            }
        }

        public async Task<IDataResult<List<BookListDTO>>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookListDTOs = books.Adapt<List<BookListDTO>>();
            if (books.Count() <= 0)
            {
                return new ErrorDataResult<List<BookListDTO>>(bookListDTOs, "Listelenecek Kitap Bulunamadı");
            }
            return new SuccesDataResult<List<BookListDTO>>(bookListDTOs, "Kitap Listeleme Başarılı");
        }

        public  async Task<IDataResult<BookDTO>> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            try
            {
                if (book == null)
                {
                    return new ErrorDataResult<BookDTO>(book.Adapt<BookDTO>(), "Güncellenecek Kitap Bulunamadı");
                }
                return new SuccesDataResult<BookDTO>(book.Adapt<BookDTO>(), "Güncellenecek Kitap Getirildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BookDTO>(book.Adapt<BookDTO>(), "Hata: " + ex.Message);
            }
        }

        public  async Task<IResult> UpdateAsync(BookUpdateDTO bookUpdateDTO)
        {
            var updatingBook = await _bookRepository.GetByIdAsync(bookUpdateDTO.Id);
            if (updatingBook == null)
            {
                return new ErrorResult("Güncellenecek Kitap Bulunamadı");
            }
            try
            {
                var updatedBook = bookUpdateDTO.Adapt(updatingBook);
                await _bookRepository.UpdateAsync(updatedBook);
                await _bookRepository.SaveChangeAsync();
                return new SuccesResult("Kitap Güncelleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata: " + ex.Message);
            }
        }

     
    }
}
