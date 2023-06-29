using Library.Application.Dtos;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("3")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdminAsync(LoginDto dto)
        {
            try
            {
                var user = await _authService.LoginAdminAsync(dto);
                return Ok(user);
            }
            catch (UserNotFoundException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUserAsync(LoginDto dto)
        {
            try
            {
                var user = await _authService.LoginUserAsync(dto);
                return Ok(user);
            }
            catch (UserNotFoundException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
