using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Models.Services.Factories.Local
{
    public class RecordingLocalStorageFactory : 
        BaseLocalFactory<IRecordingRepository>,
        IRecordingDataAccessFactory
    {
        public RecordingLocalStorageFactory(ITimeRecorderContext context) : base(context)
        {
        }

        public override IRecordingRepository GetRepository()
        {
            return new RecordingLocalRepository(context);
        }
    }
}
