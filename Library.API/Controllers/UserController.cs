using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("3")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpPost("get-book")]
        public async Task<IActionResult> GetBookAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetBookAsync(name, cancellationToken));
            }
            catch (BookNotFoundException)
            {
                return NotFound("Book not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("get-books")]
        public async Task<IActionResult> GetBooksAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetBooksAsync(cancellationToken));
            }
            catch (BookNotFoundException)
            {
                return NotFound("Book not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}