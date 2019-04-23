using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.Synchronization
{
    public class NotificationService<DTOType> : INotifyPropertyChanged
        where DTOType : LocalEntity
    {
        ICrudRepository<DTOType> online;
        ICrudRepository<DTOType> local;

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task<bool> AddListenerCreate(DTOType dto)
        {
            bool result = false;
            try {
                dto.Id = await online.CreateAsync(dto);
                result = await local.UpdateAsync(dto);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Task cancelled");
            }
            return result;

        }
    }
}