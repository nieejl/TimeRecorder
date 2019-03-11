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
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.ValueConverters;
using TimeRecorder.ViewModels.Interfaces;
using static TimeRecorder.ViewModels.RecordingOverviewVM;

namespace TimeRecorder.ViewModels
{
    public class RecordingDetailPageVM : BaseViewModel, IRecordingDetailPageVM
    {
        private TimeToStringConverter converter;
        private RecordingDTO recordingDTO;
        public RecordingDetailPageVM()
        {
            converter = new TimeToStringConverter();
        }

        public void UpdateFromDTO(RecordingDTO recording)
        {
            recordingDTO = recording;

            TimeSpan duration = (recording.EndDate - recording.StartDate) +
                (recording.EndTime - recording.StartTime);
            this.Title = recording.Title;

            this.ElapsedTime = duration.ToString();
            this.StartTime = recording.StartTime.ToString();
            this.EndTime = recording.EndTime.ToString();

            this.StartDate = recording.StartDate.ToShortDateString();
            this.EndDate = recording.EndDate.ToShortDateString();
        }

        private string title;
        public string Title {
            get { return title; }
            set {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public ICommand SaveStartTimeCommand {
            get {
                return new RelayCommand( _=>
                {
                    if (true) // TODO : Check time is ok. Change on record if yes
                        return;
                });
            }
        }

        public ICommand SaveEndTimeCommand {
            get {
                return new RelayCommand(_ =>
                {
                    if (true) // TODO : Check time is ok. Change on record if yes
                        return;
                });
            }
        }

        public SearchBoxVM<Recording> SearchVM;
        private Recording recording;

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
        // TODO: Change to do time check with converter? Add Matching for Endtime 
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
                    var recording = (RecordingDTO)rec;
                    if (recording == null)
                        return;
                    UpdateFromDTO(recording);
                });
            }
        }

        private string startTime;
        public string StartTime {
            get { return startTime; }
            set {
                startTime = value;
                OnPropertyChanged("StartTimeText");
            }
        }

        
        private void UpdateStartTimeColor() // TODO: Update with converter
        {
            var ttsc = new TimeToStringConverter();
            if (ttsc.ConvertBack(StartTime, typeof(TimeSpan), null, CultureInfo.CurrentCulture) != null) {
                StartTimeColor = new SolidColorBrush(Colors.Green);
                return;
            }
            StartTimeColor = new SolidColorBrush(Colors.Red);
        }
        
        private string endTime;
        public string EndTime {
            get { return endTime; }
            set {
                endTime = value;
                OnPropertyChanged("EndTimeText");
            }
        }

        private string elapsed;
        public string ElapsedTime {
            get { return elapsed; }
            set {
                elapsed = value;
                OnPropertyChanged("Elapsed");
            }
        }

        private string startDate;
        public string StartDate {
            get { return startDate; }
            set {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private string endDate;
        public string EndDate {
            get { return endDate; }
            set {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }
    }
}
