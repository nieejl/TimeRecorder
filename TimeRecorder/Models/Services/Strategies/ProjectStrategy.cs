using System.Collections.Generic;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.Strategies
{
    public class ProjectStrategy : 
        BaseStrategy<IProjectDataAccessFactory, IProjectRepository>,
        IProjectStrategy
    {
        public ProjectStrategy(IEnumerable<IProjectDataAccessFactory> factories) : base(factories)
        {
        }
    }
}
