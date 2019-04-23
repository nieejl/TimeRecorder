using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.Synchronization
{
    public class SynchronizationRequest<DTOType> where DTOType : LocalEntity
    {
        Action LocalRequest;
        Action OnlineRequest;
        Action CallBack;
        DTOType Dto;
        string ErrorMessage;
        bool Success;

        public SynchronizationRequest(DTOType dto, Action localRequest , Action request, Action callBack)
        {
            LocalRequest = localRequest;
            OnlineRequest = request;
            CallBack = callBack;
            Dto = dto;
        }

        public void ExecuteWithTimeout()
        {
            LocalRequest();
            try
            {
                OnlineRequest();
                Success = true;
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Request failed: " + e.Message);
            }
            CallBack();
        }

        public class SyncRequestFactory<DTOType> where DTOType : LocalEntity
        {
            ICrudRepository<DTOType> onlineRepo;
            ICrudRepository<DTOType> localRepo;

            SynchronizationRequest<DTOType> CreateRequest(DTOType dto)
            {
                return new SynchronizationRequest<DTOType>(
                    dto,
                    async () => { dto.TemporaryId = await localRepo.CreateAsync(dto); },
                    async () => dto.Id = await onlineRepo.CreateAsync(dto),
                    async () => await localRepo.UpdateAsync(dto));
            }

            SynchronizationRequest<DTOType> DeleteRequest(DTOType dto)
            {
                return new SynchronizationRequest<DTOType>(
                    dto,
                    async () => { await localRepo.DeleteAsync(dto.Id); },
                    async () => await onlineRepo.DeleteAsync(dto.Id),
                    async () => await localRepo.DeleteAsync(dto.Id)); // FULL DELETE!
            }

            SynchronizationRequest<DTOType> FindRequest(int id)
            {
                // Not really much point in this one... Just look in local and wait for sync. update.
                DTOType dto = null;
                return new SynchronizationRequest<DTOType>(
                    dto,
                    async () => { dto = await localRepo.FindAsync(id); },
                    async () => await onlineRepo.FindAsync(id),
                    async () => await localRepo.DeleteAsync(dto.Id)); // FULL DELETE!
            }

            SynchronizationRequest<DTOType> UpdateRequest(DTOType dto)
            {
                return new SynchronizationRequest<DTOType>(
                    dto,
                    async () => { var success = await localRepo.UpdateAsync(dto); },
                    async () => {
                        await localRepo.UpdateAsync(dto);
                        var result = await onlineRepo.UpdateAsync(dto);
                        // change lastupdatedserver. 
                        //if lastupdatedserver < lastupdatedlocal: local = lastupdatedserver ? 
                        //or would that cause issues with multiple updates...
                        dto.LastUpdatedServer = DateTime.MinValue;
                    },
                    async () => await localRepo.UpdateAsync(dto));
            }
        }
    }
}
