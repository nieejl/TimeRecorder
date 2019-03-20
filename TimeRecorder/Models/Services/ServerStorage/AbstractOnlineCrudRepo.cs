using System;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using System.Net.Http;
using System.Diagnostics;

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
        protected abstract string entityName { get; }

        public AbstractOnlineCrudRepo(IHttpClient client)
        {
            this.client = client;
            SetRoutes();
        }

        public virtual void SetRoutes()
        {
            basePath = "api/" + entityName;
            create = basePath + "/create/";
            find =   basePath + "/find/";
            update = basePath + "/update/";
            delete = basePath + "/delete/";
            SetCustomRoutes(basePath);
        }
        protected abstract void SetCustomRoutes(string basePath);


        public async Task<int> CreateAsync(DTOType dto)
        {
            var response = await client.PostAsJsonAsync(create, dto);
            if (response.IsSuccessAndNotNull())
            {
                dto.Id = await response.Content.ReadAsAsync<int>(client.Formatters);
                Debug.WriteLine(dto.Id);
                return dto.Id;
            }
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

        public async Task<bool> UpdateAsync(DTOType dto)
        {
            var response = await client.PutAsJsonAsync(update, dto);
            if (response.IsSuccessAndNotNull())
                return await response.Content.ReadAsAsync<bool>(client.Formatters);
            return false;
        }
    }
}
