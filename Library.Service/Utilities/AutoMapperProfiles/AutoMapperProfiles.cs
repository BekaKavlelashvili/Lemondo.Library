using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Dtos.AdministratorDto;
using Library.Application.Dtos.BookDto;
using Library.Application.Dtos.TakenBooks;
using Library.Application.Dtos.UserDto;
using Library.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Utilities.AutoMapperProfiles
{
    public static class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<User, UserDto>().ReverseMap();
                CreateMap<User, AddUserDto>().ReverseMap();
                CreateMap<User, UserToReturnDto>().ReverseMap();
                CreateMap<Administrator, AdminDto>().ReverseMap();
                CreateMap<Administrator, AdminToReturnDto>().ReverseMap();
                CreateMap<Book, CreateBookDto>().ReverseMap();
                CreateMap<Book, BookDto>().ReverseMap();
                CreateMap<Book, UpdateBookDto>().ReverseMap();
                CreateMap<Author, AuthorDto>().ReverseMap();
                CreateMap<Author, UpdateAuthorDto>().ReverseMap();
                CreateMap<TakenBooks, TakenBooksDto>().ReverseMap();
            }
        }
    }
}
