using Library.Application.Dtos;
using Library.Application.Services.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class FileService : IFileService
    {
        public async Task<FileDto> ImportAsync(IFormFile file, string directory)
        {
            var mimeType = file.ContentType;
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

            using (var fileStream = new FileStream($"App_Data/{directory}/{uniqueFileName}", FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return new FileDto
            {
                MimeType = mimeType,
                OriginalFileName = file.FileName,
                UniqueFileName = uniqueFileName,
                Path = $"App_Data/{directory}/{uniqueFileName}"
            };
        }
    }
}
