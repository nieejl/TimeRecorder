using System;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.ServerStorage;

namespace TimeRecorder.Models.Services.Factories.Online
{
    public class RecordingOnlineStorageFactory : 
        BaseOnlineFactory<IRecordingRepository>, 
        IRecordingDataAccessFactory
    {
        public RecordingOnlineStorageFactory(IHttpClient client) : base(client)
        {
        }


        public override IRecordingRepository GetRepository()
        {
            return new RecordingOnlineRepository(client);
        }
    }
}