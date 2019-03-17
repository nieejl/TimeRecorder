namespace TimeRecorder.Models.Services.Strategies
{
    public interface IStorageStrategy<Factory, Repo>
    {
        Repo CreateRepository(StorageStrategy strategy);
    }
}
