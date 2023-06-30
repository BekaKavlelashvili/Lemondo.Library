using Library.Application.Dtos;
using Library.Application.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Library.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostingEnv;

        public ImageService(IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public async Task<ImageDto> ImportAsync(IFormFile file, string directory)
        {
            if (!Directory.Exists("App_Data/BookPhotos"))
            {
                Directory.CreateDirectory("App_Data/BookPhotos");
            }

            using var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());

            var encoder = new JpegEncoder()
            {
                Quality = 100
            };

            var mimeType = file.ContentType;
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

            using (var fileStream = new FileStream($"App_Data/{directory}/{uniqueFileName}", FileMode.Create))
            {
                image.Save(fileStream, encoder);
            }

            return new ImageDto
            {
                MimeType = mimeType,
                OriginalFileName = file.FileName,
                UniqueFileName = uniqueFileName,
                Path = $"App_Data/{directory}/{uniqueFileName}"
            };
        }
    }
}
