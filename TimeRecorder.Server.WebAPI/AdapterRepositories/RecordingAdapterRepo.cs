using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Server.WebAPI.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.AdapterRepositories
{
    public class RecordingAdapterRepo :
        AbstractAdapterRepo<RecordingDTO, Recording>,
        IRecordingAdapterRepo
    {
        protected new IRecordingRepository repository;
        public RecordingAdapterRepo(IRecordingRepository repository, 
            IAdapter<RecordingDTO, Recording> adapter) : base(repository, adapter)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<RecordingDTO>> ReadAmount(int amount, int startIndex)
        {
            var entities = await repository.Read();
            return entities.OrderByDescending(r => r.Start).
                Skip(startIndex).
                Take(amount).
                Select(r => ConvertToDTO(r));
        }
    }
}
