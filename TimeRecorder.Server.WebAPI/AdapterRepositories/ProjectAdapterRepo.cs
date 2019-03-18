using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Server.WebAPI.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.AdapterRepositories
{
    public class ProjectAdapterRepo : 
        AbstractAdapterRepo<ProjectDTO, Project>, 
        IProjectAdapterRepo
    {
        public ProjectAdapterRepo(IProjectRepository repository, 
            IAdapter<ProjectDTO, Project> adapter) : base(repository, adapter)
        {
        }
    }
}
