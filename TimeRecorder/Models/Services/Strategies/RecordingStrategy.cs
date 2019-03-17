using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Models.Services.Strategies
{
    public class RecordingStrategy : BaseStrategy<IRecordingDataAccessFactory, IRecordingRepository>
    {
        public RecordingStrategy(IRecordingDataAccessFactory[] factories) : base(factories)
        {
        }
    }
}
