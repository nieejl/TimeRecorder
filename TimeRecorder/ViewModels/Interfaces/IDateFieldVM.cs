using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeRecorder.ViewModels.Interfaces
{
    public interface IDateFieldVM : INotifyPropertyChanged
    {
        SolidColorBrush BorderColor { get; }
        string TextField { get; set; }
        DateTime ParsedDate { get; set; }
        bool IsValid { get; }
    }
}
