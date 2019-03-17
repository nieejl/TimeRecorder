using Server.RepositoryLayer.Models;
using Server.RepositoryLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.RepositoryLayer.Repositories
{
    public class RecordingRepository : AbstractCrudServerRepo<Recording>, IRecordingRepository
    {
        public RecordingRepository(ITimeRecorderServerContext context) : base(context)
        {
        }
    }
}
