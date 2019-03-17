using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.ServerStorage
{
    public class ProjectOnlineRepository : AbstractOnlineCrudRepo<ProjectDTO>, IProjectRepository
    {
        public ProjectOnlineRepository(IHttpClient client) : base(client)
        {
        }

        protected override string entityName { get => "project"; }

        protected override void SetCustomRoutes(string basePath)
        {
        }
    }
}
