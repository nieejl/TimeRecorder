using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.ServerStorage;

namespace TimeRecorder.Models.Services.Factories.Online
{
    public abstract class BaseOnlineFactory<Repo> : IDataAccessFactory<Repo>
    {
        protected IHttpClient client;
        public BaseOnlineFactory(IHttpClient client)
        {
            this.client = client;
        }

        public bool CanHandle(StorageStrategy strategy)
        {
            return strategy == StorageStrategy.Online;
        }

        public abstract Repo GetRepository();
    }
}
