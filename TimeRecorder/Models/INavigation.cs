using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TimeRecorder.Models
{
    public interface INavigation
    {
        ICommand GoToRecordingOverviewCommand { get; }
        void GoTo(Type view, DependencyObject current);
    }
}
