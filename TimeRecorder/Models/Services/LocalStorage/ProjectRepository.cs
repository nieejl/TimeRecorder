using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public class ProjectRepository : AbstractCrudRepo<ProjectDTO>, IProjectRepository
    {
        public ProjectRepository(ITimeRecorcerContext context) : base(context)
        {
        }
    }
}
