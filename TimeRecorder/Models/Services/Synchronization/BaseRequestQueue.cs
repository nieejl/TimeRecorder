using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.Synchronization
{
    public class BaseRequestQueue<DTOType> where DTOType : LocalEntity
    {
        NotificationService<DTOType> notificationService;
        ICrudRepository<DTOType> onlineRepo;
        ConcurrentQueue<SynchronizationRequest<DTOType>> RequestQueue;
        public BaseRequestQueue(NotificationService<DTOType> notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task<bool> QueueCreateAsync(DTOType dto)
        {
            return await SafeRequest(dto, async item => await onlineRepo.CreateAsync(item));
        }

        public async Task<bool> QueueUpdateAsync(DTOType dto)
        {
            return await SafeRequest(dto, async item => await onlineRepo.UpdateAsync(dto));
        }

        public async Task<bool> SafeRequest(DTOType dto, Action<DTOType> action)
        {
            try
            {
                action(dto);
                return true;
            } catch (TaskCanceledException e)
            {
                Debug.WriteLine("Request failed: " + e.Message);
            }
            return false;
        }
    }
}
