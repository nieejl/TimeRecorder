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

namespace TimeRecorder.ViewModels
{
    public class RecordingDetailPageVM : BaseViewModel, IRecordingDetailPageVM
    {
        private RecordingDTO recordingDTO;
        
        public ITimeFieldVM StartTimeFieldVM { get; set; }
        public ITimeFieldVM EndTimeFieldVM { get; set; }
        public ITimeFieldVM ElapsedTimeFieldVM { get; set; }
        public IDateFieldVM StartDateFieldVM { get; set; }
        public IDateFieldVM EndDateFieldVM { get; set; }

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
            Title = recording.Title;
            StartTimeFieldVM.TextField = recording.Start.TimeOfDay.ToString();
            StartDateFieldVM.TextField = recording.Start.ToShortDateString();

            ElapsedTimeFieldVM.TextField = duration ? .ToString();
            EndTimeFieldVM.TextField = recording.End ? .TimeOfDay.ToString();
            EndDateFieldVM.TextField = recording.End ? .ToShortDateString();

            Tags = tagsAsString(recording.Tags);
        }

        private string tagsAsString(List<TagDTO> tagsAsList)
        {
            var sb = new StringBuilder();
            tagsAsList.ForEach(tag => sb.AppendFormat($"{tag.TagValue}, "));
            //foreach (var tag in tagsAsList)
            //{
            //    sb.AppendFormat($"{tag}, ");
            //}

            if (sb.Length > 0) // remove last trailing space and comma
                sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        private List<string> tagsAsStringList(string tagsAsString)
        {
            var tagsList = new List<string>();
            if (tagsAsString == null || tagsAsString.Length == 0)
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
        private bool isStartTimeAndDateValid()
        {
            return StartTimeFieldVM.IsValid && StartDateFieldVM.IsValid;
        }

        private bool isEndTimeAndDateValid()
        {
            return EndTimeFieldVM.IsValid && EndDateFieldVM.IsValid;
        }

        private void SaveToDTO()
        {
            if (recordingDTO == null)
                recordingDTO = new RecordingDTO();
            recordingDTO.Title = Title;
            recordingDTO.Start = StartDateFieldVM.ParsedDate + StartTimeFieldVM.ParsedTime;
            recordingDTO.End = EndDateFieldVM.ParsedDate + EndTimeFieldVM.ParsedTime;
            recordingDTO.Project = Project;
            recordingDTO.Tags = tagsAsStringList(Tags).
                Select(s => new TagDTO { TagValue = s }).ToList();
        }


        public SearchBoxVM<RecordingDTO> SearchVM;

        public ICommand AddNewProjectCommand {
            get {
                return new RelayCommand((name) =>
                {
                    var nameString = (string)name;
               });
            }
        }
    }
}
