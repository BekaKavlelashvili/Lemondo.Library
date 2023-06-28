using Library.Application.Dtos;
using Library.Application.Dtos.AdministratorDto;
using Library.Application.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.IServices
{
    public interface IAuthService
    {
        Task<UserToReturnDto> LoginUserAsync(LoginDto loginDto);
        Task<AdminToReturnDto> LoginAdminAsync(LoginDto loginDto);
    }
}
