using AutoMapper;
using Library.Application.Dtos.AdministratorDto;
using Library.Application.Dtos.UserDto;
using Library.Infrastructure.Entities;

namespace Library.Application.Utilities.AutoMapperProfiles
{
    public static class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<User, UserDto>().ReverseMap();
                CreateMap<User, AddOrUpdateUserDto>().ReverseMap();
                CreateMap<User, UserToReturnDto>().ReverseMap();
                CreateMap<Administrator, AdminDto>().ReverseMap();
                CreateMap<Administrator, UpdateAdminDto>().ReverseMap();
            }
        }
    }
}
