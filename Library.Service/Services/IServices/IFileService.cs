using Library.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.IServices
{
    public interface IFileService
    {
        Task<FileDto> ImportAsync(IFormFile file, string directory);
    }
}
