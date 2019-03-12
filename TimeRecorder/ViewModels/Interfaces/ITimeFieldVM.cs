using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeRecorder.ViewModels.Interfaces
{
    public interface ITimeFieldVM
    {
        SolidColorBrush BorderColor { get; }
        void Set24HourLimit(bool value);
        string TextField { get; set; }
        TimeSpan ParsedTime { get; set; }
        bool IsValid { get; }
    }
}