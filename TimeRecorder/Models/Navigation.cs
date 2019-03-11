using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TimeRecorder.Views;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace TimeRecorder.Models
{
    public class Navigation
    {

        private NavigationService asNavService(object parameter)
        {
            var frame = (NavigationService)parameter;
            if (frame == null)
                throw new ArgumentException("Frame instance was null.");
            return frame;
        }
        public Navigation()
        {
            GoToRecordingOverviewCommand = new RelayCommand(
                frame => asNavService(frame).Navigate(typeof(RecordingOverviewWindow)));
        }

        public ICommand GoToRecordingOverviewCommand { get; }
    }
}
