using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Dtos.BookDto;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Library.Infrastructure.DataContext;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using SixLabors.ImageSharp.Processing.Processors;

namespace Library.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("3")]
    [ApiController]
    [Authorize]
    [Consumes("application/json")]
    public class BookController : Controller
    {
        private readonly IAdministratorService _adminService;
        private readonly IImageService _imageService;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        private LibraryDbContext _db;

        public BookController(IAdministratorService adminService, IImageService imageService, IBookRepository bookRepository, IMapper mapper, LibraryDbContext db)
        {
            _adminService = adminService;
            _imageService = imageService;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _db = db;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add-Book")]
        public async Task<IActionResult> AddBookAsync(CreateBookDto dto)
        {
            try
            {
                return Ok(await _adminService.AddBookAsync(dto));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Roles = "admin")]
        [Consumes("multipart/form-data")]
        [HttpPost("add-file")]
        public async Task<IActionResult> AddFile([FromForm] AddPhotoDto dto)
        {
            var book = await _bookRepository.GetAsync(x => x.Id == dto.BookId);

            if (book is null)
                throw new BookNotFoundException("Book not found");

            var importResult = await _imageService.ImportAsync(dto.Image, "BookPhotos");

            book.Image = new BookImage
            {
                BookId = book.Id,
                ImageName = importResult.OriginalFileName,
                ImagePath = importResult.Path
            };

            await _bookRepository.UpdateAsync(book);

            return Ok(book);
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadAsync([FromQuery] GetBookImageDto dto)
        {
            var book = await _bookRepository.GetAsync(x => x.Id == dto.Id);

            var file = book.Image;

            string path = file.ImagePath;

            if (System.IO.File.Exists(path))
            {
                byte[] b = System.IO.File.ReadAllBytes(path);
                return File(b, "image/png");
            }

            return BadRequest("Image is not exist");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBookAsync(UpdateBookDto dto)
        {
            try
            {
                return Ok(await _adminService.UpdateBookAsync(dto));
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
                return Ok(await _adminService.GetBookAsync(bookId, cancellationToken));
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
                return Ok(await _adminService.GetBooksAsync(cancellationToken));
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
                await _adminService.DeleteBookAsync(bookId);
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
                return Ok(await _adminService.UpdateAuthorAsync(dto));
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

        [Authorize(Roles = "admin")]
        [HttpPost("get-taken-books")]
        public async Task<IActionResult> GetTakenBooksAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _adminService.GetTakenBooksAsync(cancellationToken));
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
