using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<AdministratorService> _logger;

        public AdministratorService(IUserRepository userRepository, IMapper mapper, ILogger<AdministratorService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetListAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("List of {0} has been returned", users.Count);

            return _mapper.Map<List<UserDto>>(users);
        }

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

        public async Task<UserDto> AddUserAsync(AddOrUpdateUserDto userDto)
        {
            userDto.Username = userDto.Username.ToLower();
            var addedUser = await _userRepository.AddAsync(_mapper.Map<User>(userDto));

            return _mapper.Map<UserDto>(addedUser);
        }

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
    }
}
