using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
