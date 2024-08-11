using Mapster;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Infrastructure.Repositories.AuthorRepositories;
using MVCFİnalProje.Domain.Entities;
using MVCFİnalProje.Domain.Utilities.Concretes;
using MVCFİnalProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.AuthorServices
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IResult> AddAsync(AuthorCreateDTO authorCreateDTO)
        {
            if (await _authorRepository.AnyAsync(x => x.Name.ToLower() == authorCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Yazar Sistemde Kayıtlı");
            }
            try
            {
                var newAuthor = authorCreateDTO.Adapt<Author>();
                await _authorRepository.AddAsync(newAuthor);
                await _authorRepository.SaveChangeAsync();
                return new SuccesResult("Yazar Ekleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingAuthor = await _authorRepository.GetByIdAsync(id);

            if (deletingAuthor == null)
            {
                return new ErrorResult("Silinecek Yazar Bulunumadı");
            }

            try
            {
                await _authorRepository.DeleteASync(deletingAuthor);
                await _authorRepository.SaveChangeAsync();
                return new SuccesResult("Yazar Silme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata :" + ex.Message);
            }
        }
        public async Task<IDataResult<List<AuthorListDTO>>> GetAllAsync()
        {
            
            var authors = await _authorRepository.GetAllAsync();
            var authorListDTOs = authors.Adapt<List<AuthorListDTO>>();
            if(authors.Count()<=0)
            {
                return new ErrorDataResult<List<AuthorListDTO>>(authorListDTOs, "Listelenecek Yazar Bulunamadı");
            }
            return new SuccesDataResult<List<AuthorListDTO>>(authorListDTOs, "Yazar Listeleme Başarılı");
        }

        public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            try
            {
                if (author == null)
                {
                    return new ErrorDataResult<AuthorDTO>(author.Adapt<AuthorDTO>(), "Güncellenecek Yazar Bulunamadı");
                }
                return new SuccesDataResult<AuthorDTO>(author.Adapt<AuthorDTO>(), "Güncellenecek Yazar Getirildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AuthorDTO>(author.Adapt<AuthorDTO>(), "Hata: " + ex.Message);
            }
        }

        public async Task<IResult> UpdateAsync(AuthorUpdateDTO authorUpdateDTO)
        {
            var updatingAuthor = await _authorRepository.GetByIdAsync(authorUpdateDTO.Id);
            if(updatingAuthor == null)
            {
                return new ErrorResult("Güncellenecek Yazar Bulunamadı");
            }
            try
            {
                var updatedAuthor = authorUpdateDTO.Adapt(updatingAuthor);
                await _authorRepository.UpdateAsync(updatedAuthor);
                await _authorRepository.SaveChangeAsync();
                return new SuccesResult("Yazar Güncelleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata: " + ex.Message);
            }
            
        }
    }
}

