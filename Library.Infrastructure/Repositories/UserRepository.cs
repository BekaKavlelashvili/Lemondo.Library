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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly LibraryDbContext _db;
        public UserRepository(LibraryDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _ = _db.Update(user);

            // Ignore password property update for user
            _db.Entry(user).Property(x => x.Password).IsModified = false;

            await _db.SaveChangesAsync();
            return user;
        }
    }
}
