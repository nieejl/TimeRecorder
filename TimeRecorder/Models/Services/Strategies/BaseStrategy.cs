using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TimeRecorder.Models.Services.Factories.Interfaces;

namespace TimeRecorder.Models.Services.Strategies
{
    public class BaseStrategy<Factory, Repo> : IStorageStrategy<IDataAccessFactory<Repo>, Repo>
    {
        IEnumerable<IDataAccessFactory<Repo>> factories;
        public BaseStrategy(IEnumerable<IDataAccessFactory<Repo>> factories)
        {
            if (factories == null)
                throw new ArgumentNullException("factory list was null");
            else if (factories.Count() == 0)
                throw new ArgumentException("Empty factory list");
            this.factories = factories;
        }
        public Repo CreateRepository(StorageStrategy strategy)
        {
            var factory = factories.FirstOrDefault(f => f.CanHandle(strategy));
            if (factory != null)
                return factory.GetRepository();
            else
                throw new Exception("Strategy not implemented: " + strategy.ToString());
        }
    }
}
