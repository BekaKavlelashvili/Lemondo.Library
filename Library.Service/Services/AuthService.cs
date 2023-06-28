using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Dtos.AdministratorDto;
using Library.Application.Dtos.UserDto;
using Library.Application.Services.IServices;
using Library.Application.Utilities.CustomExceptions;
using Library.Infrastructure.Repositories.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _adminRepository = adminRepository;
        }

        public async Task<UserToReturnDto> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetAsync(x => x.Username == loginDto.UserName.ToLower() && x.Password == loginDto.Password.ToLower());

            if (user == null)
                throw new UserNotFoundException();

            var userToReturn = _mapper.Map<UserToReturnDto>(user);
            userToReturn.Token = GenerateToken(user.Id, user.Username);

            return userToReturn;
        }

        public async Task<AdminToReturnDto> LoginAdminAsync(LoginDto loginDto)
        {
            var admin = await _adminRepository.GetAsync(x => x.UserName == loginDto.UserName.ToLower() && x.Password == loginDto.Password.ToLower());

            if (admin == null)
                throw new UserNotFoundException();

            var adminToReturn = _mapper.Map<AdminToReturnDto>(admin);
            adminToReturn.Token = GenerateToken(admin.Id, admin.UserName);

            return adminToReturn;
        }


        private string GenerateToken(int userId, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
