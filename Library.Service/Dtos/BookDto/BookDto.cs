using Library.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDto
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Rating { get; set; }

        public BookPhotoDto Photo { get; set; } = new BookPhotoDto();

        public BookInPdfDto PDF { get; set; } = new BookInPdfDto();

        public DateTime PublishDate { get; set; }

        public bool IsTaken { get; set; } = false;

        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    }
}
