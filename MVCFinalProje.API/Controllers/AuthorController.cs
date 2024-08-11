using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.Services.AuthorServices;

namespace MVCFinalProje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _authorService.GetAllAsync();

            if(result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }


    }
}
