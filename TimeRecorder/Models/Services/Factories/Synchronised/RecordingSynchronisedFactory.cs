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
    public class RecordingSynchronisedFactory :
        BaseSyncFactory<IRecordingRepository>,
        IRecordingDataAccessFactory
    {
        public RecordingSynchronisedFactory(ITimeRecorderContext context, IHttpClient client) : 
            base(context, client)
        {
        }

        public override IRecordingRepository GetRepository()
        {
            throw new NotImplementedException();
        }
    }
}
