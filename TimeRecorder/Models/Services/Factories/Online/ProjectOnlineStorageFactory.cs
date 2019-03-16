using TimeRecorder.Models.Services.RepositoryInterfaces;
using TimeRecorder.Models.Services.ServerStorage;

namespace TimeRecorder.Models.Services.Factories.Online
{
    public class ProjectOnlineStorageFactory : BaseOnlineFactory<IProjectRepository>
    {
        public ProjectOnlineStorageFactory(IHttpClient client) : base(client)
        {
        }

        public override IProjectRepository GetRepository()
        {
            return new ProjectOnlineRepository(client);
        }
    }
}
