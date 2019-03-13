using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<RecordingDTO>> ReadAmount(int amount, int startIndex = 0)
        {
            if (amount < 0 || startIndex < 0)
                throw new ArgumentException("ReadAmount called with negative amount or index.");
            var initial = context.Set<RecordingDTO>()
                .OrderByDescending(r => r.Start)
                .Include(r => r.Project);
            await Task.FromResult(0);
            if (startIndex <= 0)
                return initial.Take(amount);
            else
                return initial.Skip(startIndex).Take(amount);
        }
    }
}
