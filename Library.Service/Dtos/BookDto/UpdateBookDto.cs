using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDto
{
    public class UpdateBookDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Rating { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
