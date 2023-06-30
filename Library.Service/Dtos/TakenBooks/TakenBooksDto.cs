using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.TakenBooks
{
    public class TakenBooksDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public string UserFirstName { get; set; } = string.Empty;

        public string UserLastName { get; set; } = string.Empty;

        public string BookName { get; set; } = string.Empty;
    }
}
