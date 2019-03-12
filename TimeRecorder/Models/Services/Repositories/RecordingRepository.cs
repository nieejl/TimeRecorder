using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.Repositories
{
    public class RecordingRepository : AbstractCrudRepo<RecordingDTO>, IRecordingRepository
    {
        public RecordingRepository(ITimeRecorcerContext context) : base(context)
        {
        }
    }
}
