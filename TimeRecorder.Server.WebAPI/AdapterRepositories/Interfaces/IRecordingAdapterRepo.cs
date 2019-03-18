
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces
{
    public interface IRecordingAdapterRepo : IAdapterRepo<RecordingDTO, Recording>
    {
        Task<IEnumerable<RecordingDTO>> ReadAmount(int amount, int startIndex);
    }
}
