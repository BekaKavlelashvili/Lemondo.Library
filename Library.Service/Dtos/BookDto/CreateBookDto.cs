using Library.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDto
{
    public class CreateBookDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Rating { get; set; }

        public DateTime PublishDate { get; set; }

        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    }
}
