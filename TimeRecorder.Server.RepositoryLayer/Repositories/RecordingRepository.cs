using Microsoft.EntityFrameworkCore;
using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Server.RepositoryLayer.Repositories
{
    public class RecordingRepository : AbstractCrudServerRepo<Recording>, IRecordingRepository
    {
        public RecordingRepository(ITimeRecorderServerContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Recording>> ReadAmount(int amount, int startIndex)
        {
            return await context.Set<Recording>().
                Skip(startIndex).
                Take(amount).
                ToListAsync();
        }
    }
}
