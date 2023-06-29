using Library.Application.Dtos.AdministratorDto;
using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.IServices
{
    public interface IAdministratorService
    {
        //users
        Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default);

        Task<UserDto> GetUserAsync(int userId, CancellationToken cancellationToken = default);

        Task<UserDto> AddUserAsync(AddUserDto userDto);

        Task DeleteUserAsync(int userId);

        //books
        Task<BookDto> AddBookAsync(CreateBookDto bookDto);

        Task<BookDto> UpdateBookAsync(UpdateBookDto bookDto);

        Task<BookDto> GetBookAsync(int bookId, CancellationToken cancellationToken = default);

        Task<List<BookDto>> GetBooksAsync(CancellationToken cancellationToken = default);

        Task DeleteBookAsync(int bookId);

        Task<AuthorDto> UpdateAuthorAsync(UpdateAuthorDto dto);
    }
}
