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
        Task<UserDto> GetProfileAsync(CancellationToken cancellationToken = default);

        Task<UserDto> UpdateUserAsync(AddOrUpdateUserDto userDto);
    }
}
