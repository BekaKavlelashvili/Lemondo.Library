using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.IServices
{
    public interface IUserService
    {
        Task<BookDto> GetBookAsync(string name, CancellationToken cancellationToken = default);

        Task<List<BookDto>> GetBooksAsync(CancellationToken cancellationToken = default);
    }
}
