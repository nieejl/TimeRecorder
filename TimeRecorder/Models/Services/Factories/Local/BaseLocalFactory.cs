using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Models.Services.Factories.Local
{
    public abstract class BaseLocalFactory<Repo> : IDataAccessFactory<Repo>
    {
        protected ITimeRecorderContext context;
        public BaseLocalFactory(ITimeRecorderContext context)
        {
            this.context = context;
        }
        public bool CanHandle(StorageStrategy strategy)
        {
            return strategy == StorageStrategy.Local;
        }

        public abstract Repo GetRepository();
    }
}
