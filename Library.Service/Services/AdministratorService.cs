using AutoMapper;
using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.UserDto;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AdministratorService> _logger;

        public AdministratorService(IUserRepository userRepository, IMapper mapper, ILogger<AdministratorService> logger, IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        //get all users
        public async Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetListAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("List of {0} has been returned", users.Count);

            return _mapper.Map<List<UserDto>>(users);
        }


        //get user details
        public async Task<UserDto> GetUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("User with userId = {0} was requested", userId);
            var user = await _userRepository.GetAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
            {
                _logger.LogError("User with userId = {0} was not found", userId);
                throw new UserNotFoundException();
            }

            return _mapper.Map<UserDto>(user);
        }

        //add user
        public async Task<UserDto> AddUserAsync(AddUserDto userDto)
        {
            userDto.Username = userDto.Username.ToLower();
            var addedUser = await _userRepository.AddAsync(_mapper.Map<User>(userDto));

            return _mapper.Map<UserDto>(addedUser);
        }

        //delete user
        public async Task DeleteUserAsync(int userId)
        {
            var userToDelete = await _userRepository.GetAsync(x => x.Id == userId);

            if (userToDelete is null)
            {
                _logger.LogError("User with userId = {0} was not found", userId);
                throw new UserNotFoundException();
            }

            await _userRepository.DeleteAsync(userToDelete);
        }


        //add books
        public async Task<BookDto> AddBookAsync(CreateBookDto dto)
        {
            var addedBook = await _bookRepository.AddAsync(_mapper.Map<Book>(dto));

            return _mapper.Map<BookDto>(addedBook);
        }

        //update book
        public async Task<BookDto> UpdateBookAsync(UpdateBookDto dto)
        {
            var book = await _bookRepository.GetAsync(x => x.Id == dto.Id);

            if (book is null)
            {
                _logger.LogError("Book with bookId = {0} was not found", book.Id);
                throw new BookNotFoundException();
            }

            var bookToUpdate = _mapper.Map<User>(dto);

            _logger.LogInformation("Book with these properties: {@bookToUpdate} has been updated", dto);

            return _mapper.Map<BookDto>(await _userRepository.UpdateUserAsync(bookToUpdate));
        }

        //get book details
        public async Task<BookDto> GetBookAsync(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Book with id = {0} was requested", id);
            var book = await _bookRepository.GetAsync(x => x.Id == id, cancellationToken);

            if (book is null)
            {
                _logger.LogError("Book with id = {0} was not found", id);
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

        //delete book
        public async Task DeleteBookAsync(int id)
        {
            var bookToDelete = await _bookRepository.GetAsync(x => x.Id == id);

            if (bookToDelete is null)
            {
                _logger.LogError("UseBook with id = {0} was not found", id);
                throw new BookNotFoundException();
            }

            await _bookRepository.DeleteAsync(bookToDelete);
        }

        public async Task<AuthorDto> UpdateAuthorAsync(UpdateAuthorDto dto)
        {
            var book = await _bookRepository.GetAsync(x => x.Id == dto.BookId);

            if (book is null)
            {
                _logger.LogError("Book with bookId = {0} was not found", book.Id);
                throw new BookNotFoundException();
            }

            var author = book.Authors.FirstOrDefault(x=> x.Id == dto.Id);

            author.Surname = dto.Author.Surname;
            author.Name = dto.Author.Name;
            author.BirthYear = dto.Author.BirthYear;

            var authorToUpdate = _mapper.Map<Author>(author);

            return _mapper.Map<AuthorDto>(await _authorRepository.UpdateAsync(authorToUpdate));
        }
    }
}
