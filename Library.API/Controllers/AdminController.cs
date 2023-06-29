using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.UserDto;
using Library.Application.Services;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Library.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("3")]
    [ApiController]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdministratorService _service;

        public AdminController(IAdministratorService service)
        {
            _service = service;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync(AddUserDto dto)
        {
            try
            {
                return Ok(await _service.AddUserAsync(dto));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("get-user")]
        public async Task<IActionResult> GetUserAsync(int userId, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetUserAsync(userId, cancellationToken));
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("get-users")]
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetUsersAsync(cancellationToken));
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            try
            {
                await _service.DeleteUserAsync(userId);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }


        //books

        [Authorize(Roles = "admin")]
        [HttpPost("add-Book")]
        public async Task<IActionResult> AddBookAsync(CreateBookDto dto)
        {
            try
            {
                return Ok(await _service.AddBookAsync(dto));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBookAsync(UpdateBookDto dto)
        {
            try
            {
                return Ok(await _service.UpdateBookAsync(dto));
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


        [Authorize(Roles = "admin")]
        [HttpPost("get-book")]
        public async Task<IActionResult> GetBookAsync(int bookId, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetBookAsync(bookId, cancellationToken));
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost("delete-book")]
        public async Task<IActionResult> DeleteBookAsync(int bookId)
        {
            try
            {
                await _service.DeleteBookAsync(bookId);
                return Ok();
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

        [Authorize(Roles = "admin")]
        [HttpPut("update-author")]
        public async Task<IActionResult> UpdateAuthorAsync(UpdateAuthorDto dto)
        {
            try
            {
                return Ok(await _service.UpdateAuthorAsync(dto));
            }
            catch (BookNotFoundException)
            {
                return NotFound("Author not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
