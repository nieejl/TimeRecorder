using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
