using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.Repositories
{
    public interface IRecordingRepository : ICrudRepository<RecordingDTO>
    {
    }
}
