﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Rating { get; set; }

        public DateTime PublishDate { get; set; }

        public BookImage Image { get; set; } = new BookImage();

        public bool IsTaken { get; set; } = false;

        public ICollection<Author> Authors { get; set; } = new HashSet<Author>();
    }
}
