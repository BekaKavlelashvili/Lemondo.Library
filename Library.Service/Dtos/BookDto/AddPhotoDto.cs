using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDto
{
    public class AddPhotoDto
    {
        public int BookId { get; set; }

        public IFormFile Image { get; set; }
    }
}
