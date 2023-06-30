using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.UserDto;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("3")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAdministratorService _adminService;

        public UserController(IUserService userService, IAdministratorService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }


        [Authorize(Roles = "admin")]
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync(AddUserDto dto)
        {
            try
            {
                return Ok(await _adminService.AddUserAsync(dto));
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
                return Ok(await _adminService.GetUserAsync(userId, cancellationToken));
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
                return Ok(await _adminService.GetUsersAsync(cancellationToken));
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
                await _adminService.DeleteUserAsync(userId);
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
        [HttpPost("get-book")]
        public async Task<IActionResult> GetBookAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _userService.GetBookAsync(name, cancellationToken));
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
                return Ok(await _userService.GetBooksAsync(cancellationToken));
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

        [HttpPost("take-book")]
        public async Task<IActionResult> TakeBookAsync(int bookId)
        {
            try
            {
                return Ok(await _userService.TakeBookAsync(bookId));
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

        [HttpPost("return-book")]
        public async Task<IActionResult> ReturnBookAsync(int bookId)
        {
            try
            {
                return Ok(await _userService.ReturnBookAsync(bookId));
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