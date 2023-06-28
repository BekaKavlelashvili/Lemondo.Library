using Library.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UpdateUserAsync(User user);
    }
}
