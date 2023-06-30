using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Entities
{
    public class TakenBooks
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public string BookName { get; set; } = string.Empty;

        public string UserFirstName { get; set; } = string.Empty;

        public string UserLastName { get; set; } = string.Empty;

        public DateTime TakeDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
