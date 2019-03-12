using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Repositories
{
    public class RecordingRepository : IRecordingRepository
    {
        public Task<int> CreateAsync(RecordingDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RecordingDTO> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(RecordingDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
