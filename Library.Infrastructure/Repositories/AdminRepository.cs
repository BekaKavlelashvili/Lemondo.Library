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
    public class AdminRepository : GenericRepository<Administrator>, IAdminRepository
    {
        private readonly LibraryDbContext _db;
        public AdminRepository(LibraryDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Administrator> UpdateAdminAsync(Administrator admin)
        {
            _ = _db.Update(admin);

            // Ignore password property update for user
            _db.Entry(admin).Property(x => x.Password).IsModified = false;

            await _db.SaveChangesAsync();
            return admin;
        }
    }
}
