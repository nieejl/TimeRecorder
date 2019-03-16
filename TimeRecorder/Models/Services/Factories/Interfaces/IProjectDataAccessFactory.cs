using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.Factories.Interfaces
{
    public interface IProjectDataAccessFactory : 
        IDataAccessFactory<IProjectRepository>
    {
    }
}
