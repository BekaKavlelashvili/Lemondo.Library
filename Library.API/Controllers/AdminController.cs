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
        public async Task<IActionResult> AddUserAsync(AddOrUpdateUserDto dto)
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
    }
}
