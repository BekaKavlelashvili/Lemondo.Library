using AutoMapper;
using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.UserDto;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _bookRepository = bookRepository;
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

    }
}
