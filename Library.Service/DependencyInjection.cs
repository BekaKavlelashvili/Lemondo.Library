using Library.Application.Services.IServices;
using Library.Application.Services;
using Library.Application.Utilities.Swagger;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Library.Application.Utilities.AutoMapperProfiles;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using Library.Application.Utilities.Validators;
using Library.Infrastructure.Repositories.IRepositories;
using Library.Infrastructure.Repositories;

namespace Library.Application
{
    public static class DependencyInjection
    {
        public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();

            #region Versioning

            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(3, 0);
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            #endregion

            #region Swagger

            services.AddSwaggerGen(options =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Paste your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            services.ConfigureOptions<SwaggerConfigurationOptions>();

            #endregion        
        }
    }
}