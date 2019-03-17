using System.Collections.Generic;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Models.Services.Strategies
{
    public class RecordingStrategy : 
        BaseStrategy<IRecordingDataAccessFactory, IRecordingRepository>,
        IRecordingStrategy
    {
        public RecordingStrategy(IEnumerable<IRecordingDataAccessFactory> factories) : base(factories)
        {
        }
    }
}
