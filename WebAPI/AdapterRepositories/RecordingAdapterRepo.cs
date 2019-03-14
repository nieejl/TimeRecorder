using Server.RepositoryLayer.Models.Entities;
using Server.RepositoryLayer.Repositories;
using Server.WebAPI.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace Server.WebAPI.AdapterRepositories
{
    public class RecordingAdapterRepo :
        AbstractAdapterRepo<RecordingDTO, Recording>
    {
        public RecordingAdapterRepo(IRepository<Recording> repository, 
            IAdapter<RecordingDTO, Recording> adapter) : base(repository, adapter)
        {
        }
    }
}
