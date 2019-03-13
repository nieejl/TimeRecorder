using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.ViewModels.Interfaces
{
    public interface IRecordingDetailPageVM : INotifyPropertyChanged
    {
        void UpdateFromDTO(int dtoID);
    }
}
