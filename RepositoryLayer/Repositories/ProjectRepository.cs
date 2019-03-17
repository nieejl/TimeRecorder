using Server.RepositoryLayer.Models;
using Server.RepositoryLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.RepositoryLayer.Repositories
{
    public class ProjectRepository : AbstractCrudServerRepo<Project>, IProjectRepository
    {
        public ProjectRepository(ITimeRecorderServerContext context) : base(context)
        {
        }
    }
}
