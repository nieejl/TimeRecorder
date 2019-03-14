using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RepositoryLayer.Repositories
{
    public interface IRepository<T>
    {
        Task<int> CreateAsync(T dto);
        Task<bool> DeleteAsync(int id);
        Task<T> FindAsync(int id);
        Task<bool> UpdateAsync(T dto);
        Task<IQueryable<T>> Read();
    }
}
