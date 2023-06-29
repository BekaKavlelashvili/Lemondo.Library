using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDto
{
    public class UpdateAuthorDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public AuthorDto Author { get; set; } = new AuthorDto();
    }
}
