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
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels.Interfaces;
using static TimeRecorder.ViewModels.RecordingOverviewVM;

namespace TimeRecorder.ViewModels
{
    public class RecordingDetailPageVM : BaseViewModel, IRecordingDetailPageVM
    {
        private RecordingDTO recordingDTO;
        
        public ITimeFieldVM StartTimeFieldVM { get; private set; }
        public ITimeFieldVM EndTimeFieldVM { get; private set; }
        public ITimeFieldVM ElapsedTimeFieldVM { get; private set; }
        public IDateFieldVM StartDateFieldVM { get; private set; }
        public IDateFieldVM EndDateFieldVM { get; private set; }

        public RecordingDetailPageVM(IParserFieldVMFactory fieldVMFactory)
        {
            StartTimeFieldVM = fieldVMFactory.Generate24HourTimeField();
            EndTimeFieldVM = fieldVMFactory.Generate24HourTimeField();
            ElapsedTimeFieldVM = fieldVMFactory.GenerateUnlimitedTimeField();
            StartDateFieldVM = fieldVMFactory.GenerateDateField();
            EndDateFieldVM = fieldVMFactory.GenerateDateField();

        }

        private string title;
        public string Title {
            get { return title; }
            set {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private ProjectDTO project;
        public ProjectDTO Project {
            get { return project; }
            set {
                project = value;
                OnPropertyChanged("Project");
            }
        }

        private string tags;
        public string Tags {
            get { return tags; }
            set {
                tags = value;
                OnPropertyChanged("Tags");
            }
        }

        public void UpdateFromDTO(RecordingDTO recording)
        {
            recordingDTO = recording;

            TimeSpan? duration = recording.End - recording.Start;
            this.Title = recording.Title;
            this.StartTimeFieldVM.TextField = recording.Start.TimeOfDay.ToString();
            this.StartDateFieldVM.TextField = recording.Start.ToShortDateString();

            this.ElapsedTimeFieldVM.TextField = duration ? .ToString();
            this.EndTimeFieldVM.TextField = recording.End ? .TimeOfDay.ToString();
            this.EndDateFieldVM.TextField = recording.End ? .ToShortDateString();

            this.Tags = tagsAsString(recording.Tags);
        }

        private string tagsAsString(List<string> tagsAsList)
        {
            var sb = new StringBuilder();
            tagsAsList.ForEach(tag => sb.AppendFormat("%s, ", tag));
            if (sb.Length > 0) // remove last trailing space and comma
                sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        private List<string> tagsAsList(string tagsAsString)
        {
            var tagsList = new List<string>();
            if (tagsAsString.Length == 0)
                return tagsList;
            var seperatedTags = tagsAsString.Split(
                new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            tagsList.AddRange(seperatedTags);
            return tagsList;
        }

        public bool TrySaveToDTO()
        {
            if (isStartTimeAndDateValid() && isEndTimeAndDateValid())
            {
                SaveToDTO();
                return true;
            }
            return false;
        }

        public void SaveToDTO()
        {
            recordingDTO.Title = Title;
            recordingDTO.Start = StartDateFieldVM.ParsedDate + StartTimeFieldVM.ParsedTime;
            recordingDTO.End = EndDateFieldVM.ParsedDate + EndTimeFieldVM.ParsedTime;
            recordingDTO.Project = Project;
            recordingDTO.Tags = tagsAsList(Tags);
        }

        private bool isStartTimeAndDateValid()
        {
            return StartTimeFieldVM.IsValid && StartDateFieldVM.IsValid;
        }

        private bool isEndTimeAndDateValid()
        {
            return EndTimeFieldVM.IsValid && EndDateFieldVM.IsValid;
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

        public ICommand AddNewProjectCommand {
            get {
                return new RelayCommand((name) =>
                {
                    var nameString = (string)name;
               });
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
    }
}
