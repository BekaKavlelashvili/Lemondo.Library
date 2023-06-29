using Library.Infrastructure.DataContext;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _db;

        public BookRepository(LibraryDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _ = _db.Update(book);

            await _db.SaveChangesAsync();
            return book;
        }
    }
}
