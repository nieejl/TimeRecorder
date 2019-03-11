using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimeRecorder.ViewModels.Interfaces
{
    public interface IRecordingOverviewVM : INotifyPropertyChanged
    {
        ICommand LoadMoreCommand { get; }
        ICommand RemoveEntryCommand { get; }
        ICommand ToggleTimerCommand { get; }

    }
}
