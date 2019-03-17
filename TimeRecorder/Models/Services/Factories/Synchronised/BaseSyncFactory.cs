using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.ServerStorage;

namespace TimeRecorder.Models.Services.Factories.Synchronised
{
    public abstract class BaseSyncFactory<Repo> : IDataAccessFactory<Repo>
    {
        protected ITimeRecorderContext context;
        protected IHttpClient client;
        public BaseSyncFactory(ITimeRecorderContext context, IHttpClient client)
        {
            this.context = context;
            this.client = client;
        }
        public bool CanHandle(StorageStrategy strategy)
        {
            return strategy == StorageStrategy.Synchronised;
        }

        public abstract Repo GetRepository();
    }
}
