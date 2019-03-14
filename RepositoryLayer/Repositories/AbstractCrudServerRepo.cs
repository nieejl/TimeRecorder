using Server.RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RepositoryLayer.Repositories
{
    public class AbstractCrudServerRepo<T> where T : Entity
    {
        protected ITimeRecorderServerContext context;
        public AbstractCrudServerRepo(ITimeRecorderServerContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAsync(T dto)
        {
            await context.Set<T>().AddAsync(dto);
            await context.SaveChangesAsync();
            return dto.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var set = context.Set<T>();
            var item = await set.FindAsync(id);

            if (item == null)
                return false;

            set.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<T> FindAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T dto)
        {
            var id = dto.Id;
            var set = context.Set<T>();
            var item = await set.FindAsync(id);
            if (item == null)
                return false;
            set.Update(dto);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<T>> Read()
        {
            return await Task.FromResult(context.Set<T>().Select(item => item));
        }
    }
}
