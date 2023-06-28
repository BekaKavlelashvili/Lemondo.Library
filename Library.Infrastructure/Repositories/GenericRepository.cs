using Library.Infrastructure.DataContext;
using Library.Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly LibraryDbContext _db;

        public GenericRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entity)
        {
            await _db.AddRangeAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _ = _db.Remove(entity);
            return await _db.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await (filter == null ? _db.Set<TEntity>().ToListAsync(cancellationToken) : _db.Set<TEntity>().Where(filter).ToListAsync(cancellationToken));
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _ = _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entity)
        {
            _db.UpdateRange(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
