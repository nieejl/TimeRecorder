using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TimeRecorder.Models;
using TimeRecorder.Models.ValueConverters;
using TimeRecorder.ViewModels.Interfaces;
using static TimeRecorder.ViewModels.RecordingOverviewVM;

namespace TimeRecorder.ViewModels
{
    public class RecordingDetailPageVM : BaseViewModel, IRecordingDetailPageVM
    {
        public RecordingDetailPageVM()
        {
            Recording = new Recording
            {
                Duration = "10:0:02",
                Description = "Derp",
                ProjectName = "PORJ"
            };
            StartTime = new TimeSpan(10, 2, 3);
        }

        public SearchBoxVM<Recording> SearchVM;
        private Recording recording;
        public Recording Recording {
            get { return recording; }
            set {
                recording = value;
                OnPropertyChanged("Recording");
                Debug.WriteLine(recording.Description + recording.Duration + recording.ProjectName);
            }
        }

        public ICommand AddNewProjectCommand {
            get {
                return new RelayCommand((name) =>
                {
                    var nameString = (string)name;
                    Debug.WriteLine(recording.Description + recording.Duration + recording.ProjectName);
                    Debug.WriteLine(StartTime);
                    Debug.WriteLine(StartTimeColor);
               });
            }
        }

        private SolidColorBrush _startTimeColor;
        public SolidColorBrush StartTimeColor {
            get { return _startTimeColor; }
            set {
                _startTimeColor = value;
                OnPropertyChanged("StartTimeColor");
            }
        }


        public ICommand LoadDetailsCommand {
            get {
                return new RelayCommand((rec) =>
                {
                    var recording = (Recording)rec;
                    if (recording == null)
                        return;
                    ElapsedTimeText = recording.Duration;
                });
            }
        }

        private string startTimeText = "10:00:00";
        public string StartTimeText {
            get { return startTimeText; }
            set {
                startTimeText = value;
                OnPropertyChanged("StartTimeText");
            }
        }

        private void UpdateStartTimeColor()
        {
            var ttsc = new TimeToStringConverter();
            if (ttsc.ConvertBack(StartTimeText, typeof(TimeSpan), null, CultureInfo.CurrentCulture) != null) {
                StartTimeColor = new SolidColorBrush(Colors.Green);
                return;
            }
            StartTimeColor = new SolidColorBrush(Colors.Red);
        }

        private string _elapsedTimeText;
        public string ElapsedTimeText {
            get { return _elapsedTimeText; }
            set {
                _elapsedTimeText = value;
                OnPropertyChanged("ElapsedTimeText");
            }
        }

        private string endTimeText;
        public string EndTimeText {
            get { return endTimeText; }
            set {
                endTimeText = value;
                OnPropertyChanged("EndTimeText");
            }
        }

        private TimeSpan startTime;
        public TimeSpan StartTime {
            get { return startTime; }
            set {
                startTime = value;
                OnPropertyChanged("Start");
                UpdateStartTimeColor();
            }
        }

        private TimeSpan endTime;
        public TimeSpan EndTime {
            get { return endTime; }
            set {
                endTime = value;
                OnPropertyChanged("End");
            }
        }

        private TimeSpan elapsed;
        public TimeSpan Elapsed {
            get { return elapsed; }
            set {
                elapsed = value;
                OnPropertyChanged("Elapsed");
            }
        }

        private DateTime startDate;
        public DateTime StartDate {
            get { return startDate; }
            set {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime endDate;
        public DateTime EndDate {
            get { return endDate; }
            set {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public void updateElapsed()
        {
            if (StartTime != null && EndTime != null)
                Elapsed = EndTime - startTime;
        }
    }
}
