using Library.Application.Dtos.AdministratorDto;
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
        Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default);

        Task<UserDto> GetUserAsync(int userId, CancellationToken cancellationToken = default);

        Task<UserDto> AddUserAsync(AddOrUpdateUserDto userDto);

        Task DeleteUserAsync(int userId);
    }
}
