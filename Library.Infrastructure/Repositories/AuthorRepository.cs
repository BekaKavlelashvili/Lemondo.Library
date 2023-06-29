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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly LibraryDbContext _db;

        public AuthorRepository(LibraryDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            _ = _db.Update(author);

            await _db.SaveChangesAsync();
            return author;
        }
    }
}
