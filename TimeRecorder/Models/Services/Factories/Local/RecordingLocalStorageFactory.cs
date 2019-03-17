using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Models.Services.Factories.Local
{
    public class RecordingLocalStorageFactory : BaseLocalFactory<IRecordingRepository>
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
