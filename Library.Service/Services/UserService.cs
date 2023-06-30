using AutoMapper;
using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.UserDto;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ITakenBookRepository _takenBookRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, IBookRepository bookRepository, ITakenBookRepository takenBookRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _bookRepository = bookRepository;
            _takenBookRepository = takenBookRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        //get book details
        public async Task<BookDto> GetBookAsync(string name, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Book with name = {0} was requested", name);
            var book = await _bookRepository.GetAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);

            if (book is null)
            {
                _logger.LogError("Book with name = {0} was not found", name);
                throw new BookNotFoundException();
            }

            return _mapper.Map<BookDto>(book);
        }

        //get all books
        public async Task<List<BookDto>> GetBooksAsync(CancellationToken cancellationToken = default)
        {
            var books = await _bookRepository.GetListAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("List of {0} has been returned", books.Count);

            return _mapper.Map<List<BookDto>>(books);
        }

        //take book
        public async Task<BookDto> TakeBookAsync(int bookId, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.GetAsync(x => x.Id == bookId && !x.IsTaken, cancellationToken);
            if (book is null)
            {
                _logger.LogError("Book with id = {0} was not found", bookId);
                throw new BookNotFoundException();
            }

            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            var user = await _userRepository.GetAsync(x => x.Username == username);

            var taken = await _takenBookRepository.AddAsync(new TakenBooks
            {
                UserId = user.Id,
                BookId = bookId,
                TakeDate = DateTime.UtcNow,
                BookName = book.Name,
                UserFirstName = user.Name,
                UserLastName = user.Surname
            });

            _mapper.Map<TakenBooks>(taken);

            book.IsTaken = true;
            var bookToUpdate = _mapper.Map<Book>(book);

            return _mapper.Map<BookDto>(await _bookRepository.UpdateAsync(bookToUpdate));
        }

        //return book
        public async Task<BookDto> ReturnBookAsync(int bookId, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.GetAsync(x => x.Id == bookId && x.IsTaken, cancellationToken);
            if (book is null)
            {
                _logger.LogError("Book with id = {0} was not found", bookId);
                throw new BookNotFoundException();
            }

            book.IsTaken = false;

            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            var user = await _userRepository.GetAsync(x => x.Username == username);

            var takenBook = await _takenBookRepository.GetAsync(x => x.UserId == user.Id && x.BookId == book.Id);
            if (takenBook is null)
            {
                _logger.LogError("Taken Book with book id = {0} was not found", bookId);
                throw new BookNotFoundException();
            }

            takenBook.ReturnDate = DateTime.UtcNow;

            await _takenBookRepository.UpdateAsync(takenBook);
            _mapper.Map<TakenBooks>(takenBook);

            var bookToUpdate = _mapper.Map<Book>(book);

            return _mapper.Map<BookDto>(await _bookRepository.UpdateAsync(bookToUpdate));
        }
    }
}
