using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public class AbstractCrudRepo<T> : ICrudRepository<T> where T: LocalEntity
    {
        protected ITimeRecorcerContext context;
        public AbstractCrudRepo(ITimeRecorcerContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
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

        public async Task<bool> UpdateAsync(T entity)
        {
            var set = context.Set<T>();
            var item = await set.FindAsync(entity.Id);
            if (item == null)
                return false;
            set.Update(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<T>> Read()
        {
            return await Task.FromResult(context.Set<T>().Select(item => item));
        }
    }
}
