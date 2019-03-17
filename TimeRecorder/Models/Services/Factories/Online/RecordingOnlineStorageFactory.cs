using System;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.ServerStorage;

namespace TimeRecorder.Models.Services.Factories.Online
{
    public class RecordingOnlineStorageFactory : IRecordingDataAccessFactory
    {
        IHttpClient client;
        public RecordingOnlineStorageFactory(IHttpClient client)
        {
            this.client = client;
        }

        public bool CanHandle(StorageStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public IRecordingRepository GetRepository()
        {
            return new RecordingOnlineRepository(client);
        }
    }
}