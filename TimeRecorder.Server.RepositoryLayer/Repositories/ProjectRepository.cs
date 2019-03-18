using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeRecorder.Server.RepositoryLayer.Repositories
{
    public class ProjectRepository : AbstractCrudServerRepo<Project>, IProjectRepository
    {
        public ProjectRepository(ITimeRecorderServerContext context) : base(context)
        {
        }
    }
}
