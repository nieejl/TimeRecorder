using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public class RecordingLocalRepository : AbstractCrudRepo<RecordingDTO>, IRecordingRepository
    {
        public RecordingLocalRepository(ITimeRecorderContext context) : base(context)
        {
        }
        public async Task<IEnumerable<RecordingDTO>> ReadAmount(int amount, int startIndex = 0)
        {
            if (amount < 0 || startIndex < 0)
                throw new ArgumentException("ReadAmount called with negative amount or index.");
            var initial = context.Set<RecordingDTO>()
                .OrderByDescending(r => r.Start)
                .Include(r => r.Project);
            if (startIndex <= 0)
                return await initial.Take(amount).ToListAsync();
            else
                return await initial.Skip(startIndex).Take(amount).ToListAsync();
        }
    }
}
