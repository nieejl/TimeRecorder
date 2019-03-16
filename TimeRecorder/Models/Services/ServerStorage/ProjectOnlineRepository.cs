using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.ServerStorage
{
    public class ProjectOnlineRepository : AbstractOnlineCrudRepo<ProjectDTO>, IProjectRepository
    {
        public ProjectOnlineRepository(IHttpClient client) : base(client)
        {
        }

        protected override void SetCustomRoutes(string basePath)
        {
        }
    }
}
