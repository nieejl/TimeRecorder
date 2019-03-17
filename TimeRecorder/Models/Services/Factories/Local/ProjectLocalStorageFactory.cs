using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.Factories.Local
{
    public class ProjectLocalStorageFactory : BaseLocalFactory<IProjectRepository>
    {
        public ProjectLocalStorageFactory(ITimeRecorderContext context) : base(context)
        {
        }

        public override IProjectRepository GetRepository()
        {
            return new ProjectLocalRepository(context);
        }
    }
}
