using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeRecorder.ViewModels
{
    public class RecordingSummaryVM : BaseViewModel
    {
        public int Id { get; set; }
        private string _title;
        public string Title {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChanged();
            }
        }
        private string _projectName;
        public string ProjectName {
            get { return _projectName; }
            set {
                _projectName = value;
                OnPropertyChanged();
            }
        }

        private string duration;
        public string Duration {
            get { return duration; }
            set {
                duration = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush projectColor;
        public SolidColorBrush ProjectColor {
            get { return projectColor; }
            set {
                projectColor = value;
                OnPropertyChanged();
            }
        }
    }
}
