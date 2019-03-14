using Server.RepositoryLayer.Models.Entities;
using Server.RepositoryLayer.Repositories;
using Server.WebAPI.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace Server.WebAPI.AdapterRepositories
{
    public class ProjectAdapterRepo : AbstractAdapterRepo<ProjectDTO, Project>
    {
        public ProjectAdapterRepo(IRepository<Project> repository, 
            IAdapter<ProjectDTO, Project> adapter) : base(repository, adapter)
        {
        }
    }
}
