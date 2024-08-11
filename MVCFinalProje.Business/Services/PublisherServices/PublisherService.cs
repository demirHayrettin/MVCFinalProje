﻿using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Infrastructure.Repositories.PublisherRepository;
using MVCFİnalProje.Domain.Utilities.Concretes;
using MVCFİnalProje.Domain.Utilities.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCFİnalProje.Domain.Entities;

namespace MVCFinalProje.Business.Services.PublisherServices
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<IResult> AddAsync(PublisherCreateDTO publisherCreateDTO)
        {
            if(await _publisherRepository.AnyAsync(x => x.Name.ToLower() == publisherCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Yayınevi Sistemde Kayıtlı");
            }

            try
            {
                var newPublisher = publisherCreateDTO.Adapt<Publisher>();
                await _publisherRepository.AddAsync(newPublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccesResult("Yayınevi Ekleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata :" + ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingPublisher = await _publisherRepository.GetByIdAsync(id);
            if (deletingPublisher == null)
            {
                return new ErrorResult("Silinecek Yayınevi Bulunamadı");
            }

            try
            {
                await _publisherRepository.DeleteASync(deletingPublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccesResult("Yayınevi Silme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata :" + ex.Message);
            }
        }

        public async Task<IDataResult<List<PublisherListDTO>>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAllAsync(x => x.CreatedDate, false); // Oluşturma tarihine göre eskiden yeniye
            var publisherListDTOs = publishers.Adapt<List<PublisherListDTO>>();
            if(publishers.Count()<=0)
            {
                return new ErrorDataResult<List<PublisherListDTO>>(publisherListDTOs, "Listelenecek Yayınevi Bulunamadı");
            }
            return new SuccesDataResult<List<PublisherListDTO>>(publisherListDTOs, "Yayınevi Listeleme Başarılı");
        }

        public async Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if(publisher == null)
            {
                return new ErrorDataResult<PublisherDTO>(publisher.Adapt<PublisherDTO>(), "Yayınevi Bulunamadı");
            }
            return new SuccesDataResult<PublisherDTO>(publisher.Adapt<PublisherDTO>(), "Yayınevi Bulundu");
        }

        public async Task<IResult> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO)
        {
          var updatingPublisher = await _publisherRepository.GetByIdAsync(publisherUpdateDTO.Id);
            if(updatingPublisher == null)
            {
                return new ErrorResult("Güncellenecek Yayınevi Bulunamadı");
            }

            try
            {
                var updatePublisher = publisherUpdateDTO.Adapt(updatingPublisher);
                await _publisherRepository.UpdateAsync(updatePublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccesResult("Yayınevi Güncelleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata :" + ex.Message);
            }
        }
    }
}
