using System;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using System.Net.Http;

namespace TimeRecorder.Models.Services.ServerStorage
{
    /// <summary>
    /// Basic implementation of all CRUD operations, for communication with Backend.
    /// </summary>
    /// <typeparam name="DTOType"></typeparam>
    public abstract class AbstractOnlineCrudRepo<DTOType> : 
        ICrudRepository<DTOType>
        where DTOType : LocalEntity
    {
        protected IHttpClient client;
        protected string basePath;
        protected string create;
        protected string find;
        protected string update;
        protected string delete;
        protected string read;
        protected abstract string entityName { get; }

        public virtual void SetRoutes()
        {
            basePath = "api/" + entityName;
            create = basePath + "/create/";
            find =   basePath + "/find/";
            update = basePath + "/update/";
            delete = basePath + "/delete/";
            read = basePath + "/read/";
            SetCustomRoutes(basePath);
        }

        protected abstract void SetCustomRoutes(string basePath);

        public AbstractOnlineCrudRepo(IHttpClient client)
        {
            this.client = client;
            SetRoutes();
        }

        public async Task<int> CreateAsync(DTOType dto)
        {
            var response = await client.GetAsync(create);
            if (response.IsSuccessAndNotNull())
                return await response.Content.ReadAsAsync<int>(client.Formatters);
            return 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await client.DeleteAsync(delete + id);
            if (response.IsSuccessAndNotNull())
                return await response.Content.ReadAsAsync<bool>(client.Formatters);
            return false;
        }

        public async Task<DTOType> FindAsync(int id)
        {
            var response = await client.GetAsync(find + id);
            if (response.IsSuccessAndNotNull())
                return await response.Content.ReadAsAsync<DTOType>(client.Formatters);
            return null;
        }

        public async Task<IQueryable<DTOType>> Read()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(DTOType dto)
        {
            var response = await client.DeleteAsync(update);
            if (response.IsSuccessAndNotNull())
                return await response.Content.ReadAsAsync<bool>(client.Formatters);
            return false;
        }
    }
}
