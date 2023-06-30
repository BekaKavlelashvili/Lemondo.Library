using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Entities
{
    public class BookImage
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string? ImageName { get; set; }

        public string? ImagePath { get; set; }
    }
}
