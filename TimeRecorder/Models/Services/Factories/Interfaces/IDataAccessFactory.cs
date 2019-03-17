namespace TimeRecorder.Models.Services.Factories.Interfaces
{
    public interface IDataAccessFactory<Repo>
    {
        Repo GetRepository();
        bool CanHandle(StorageStrategy strategy);
    }
}
