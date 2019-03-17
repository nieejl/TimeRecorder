using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.Strategies
{
    public class ProjectStrategy : BaseStrategy<IProjectDataAccessFactory, IProjectRepository>
    {
        public ProjectStrategy(IDataAccessFactory<IProjectRepository>[] factories) : base(factories)
        {
        }
    }
}
