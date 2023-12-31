﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Utilities.CustomExceptions
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException()
        {
        }

        public BookNotFoundException(string message)
            : base(message)
        {
        }

        public BookNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
