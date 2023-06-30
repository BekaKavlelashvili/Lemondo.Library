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
    public class TakenBooksRepository : GenericRepository<TakenBooks>, ITakenBookRepository
    {
        private readonly LibraryDbContext _db;

        public TakenBooksRepository(LibraryDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<TakenBooks> UpdateTakenBookAsync(TakenBooks taken)
        {
            _ = _db.Update(taken);

            await _db.SaveChangesAsync();
            return taken;
        }
    }
}
