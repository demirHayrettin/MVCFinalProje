using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Business.Services.PublisherServices;
using MVCFİnalProje.Domain.Utilities.Concretes;

namespace MVCFinalProje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _publisherService.GetAllAsync();

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _publisherService.GetByIdAsync(id);

            if (result is null || !result.IsSucces)
            {
                return NotFound(result?.Message ?? "Publisher not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PublisherCreateDTO publisherCreateDTO)
        {
            var result = await _publisherService.AddAsync(publisherCreateDTO);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] PublisherUpdateDTO publisherUpdateDTO)
        {
            //// İlk olarak verilen ID ile publisher'ı getir
            //var existingPublisherResult = await _publisherService.GetByIdAsync(id);

            //// Eğer publisher bulunamazsa, NotFound döndür
            //if (existingPublisherResult is null || !existingPublisherResult.IsSucces)
            //{
            //    return NotFound(existingPublisherResult?.Message ?? "Publisher not found.");
            //}

            //// Gelen veriyi güncelleme DTO'suna aktar
            //var existingPublisher = existingPublisherResult.Data;
            //existingPublisher.Adapt(publisherUpdateDTO);

            // Güncellenmiş veriyi kullanarak update işlemi yap
            var updateResult = await _publisherService.UpdateAsync(publisherUpdateDTO);

            // Eğer update işlemi başarısız olursa, BadRequest döndür
            if (updateResult is null || !updateResult.IsSucces)
            {
                return BadRequest(updateResult?.Message ?? "An error occurred while updating the publisher.");
            }

            // Başarılı ise Ok döndür
            return Ok(updateResult);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _publisherService.DeleteAsync(id);

            if (result is null || !result.IsSucces)
            {
                return BadRequest(result?.Message ?? "An error occurred while deleting the publisher.");
            }

            return Ok(result);
        }
    }


}
