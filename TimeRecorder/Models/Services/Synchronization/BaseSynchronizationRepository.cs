using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.Synchronization
{
    public class BaseSynchronizationRepository<DTOType> : ICrudRepository<DTOType> where DTOType : LocalEntity
    {
        ICrudRepository<DTOType> local;
        ICrudRepository<DTOType> online;
        NotificationService<DTOType> notificationService;

        public BaseSynchronizationRepository (NotificationService<DTOType> notificationService,
            ICrudRepository<DTOType> local, ICrudRepository<DTOType> online)
        {
            this.notificationService = notificationService;
            this.local = local;
            this.online = online;
        }

        public async Task<int> CreateAsync(DTOType dto)
        {
            // save locally, send online save-request, when response arrives, save the necessary.
            dto.TemporaryId = await local.CreateAsync(dto);
            await notificationService.AddListenerCreate(dto);
            return dto.TemporaryId;
        }

        public Task<bool> DeleteAsync(int id)
        {
            // mark for deletion locally
            // inform online
            throw new NotImplementedException();
        }

        public Task<DTOType> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(DTOType dto)
        {
            throw new NotImplementedException();
        }
    }
}
