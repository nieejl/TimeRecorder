using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public class ProjectLocalRepository : AbstractCrudRepo<ProjectDTO>, IProjectRepository
    {
        public ProjectLocalRepository(ITimeRecorderContext context) : base(context)
        {
        }
    }
}
