using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces
{
    public interface IProjectAdapterRepo : IAdapterRepo<ProjectDTO, Project>
    {

    }
}
