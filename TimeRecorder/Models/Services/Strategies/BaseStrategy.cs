using System;
using System.Linq;
using TimeRecorder.Models.Services.Factories.Interfaces;

namespace TimeRecorder.Models.Services.Strategies
{
    public class BaseStrategy<Factory, Repo> : IStorageStrategy<IDataAccessFactory<Repo>, Repo>
    {
        IDataAccessFactory<Repo>[] factories;
        public BaseStrategy(IDataAccessFactory<Repo>[] factories)
        {
            this.factories = factories;
        }
        public Repo CreateRepository(StorageStrategy strategy)
        {
            var factory = factories.FirstOrDefault(f => f.CanHandle(strategy));
            if (factory != null)
                return factory.GetRepository();
            else
                throw new Exception("Recording strategy not implemented");
        }
    }
}
