using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public interface IRecordingRepository : ICrudRepository<RecordingDTO>
    {
        Task<IEnumerable<RecordingDTO>> ReadAmount(int amount, int startIndex = 0);
    }
}
