using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.ServerStorage
{
    public class ProjectOnlineRepository : AbstractOnlineCrudRepo<ProjectDTO>, IProjectRepository
    {
        protected string read;
        public ProjectOnlineRepository(IHttpClient client) : base(client)
        {
        }

        protected override string entityName { get => "project"; }

        protected override void SetCustomRoutes(string basePath)
        {
            read = basePath + "/read/";
        }

        public async Task<IEnumerable<ProjectDTO>> Read()
        {
            var response = await client.GetAsync(read);
            if (response.IsSuccessAndNotNull())
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<IEnumerable<ProjectDTO>>(result);
                //return await response.Content.ReadAsAsync<IEnumerable<ProjectDTO>>();
            }//return await response.Content.ReadAsAsync<IEnumerable<ProjectDTO>>(client.Formatters);
            return new List<ProjectDTO>();
        }
    }
}
