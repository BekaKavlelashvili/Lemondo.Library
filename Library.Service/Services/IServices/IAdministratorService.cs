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
        Task<AdminDto> GetAdminInfo(CancellationToken cancellationToken = default);

        Task<AdminDto> UpdateAdminAsync(UpdateAdminDto adminDto);

        Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default);

        Task<UserDto> GetUserAsync(Guid entityId, CancellationToken cancellationToken = default);

        Task<UserDto> AddUserAsync(AddOrUpdateUserDto userDto);

        Task<UserDto> UpdateUserAsync(AddOrUpdateUserDto userDto);

        Task<UserDto> DeleteUserAsync(Guid entityId);
    }
}
