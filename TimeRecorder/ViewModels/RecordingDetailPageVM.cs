﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TimeRecorder.Models;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services.Repositories;
using TimeRecorder.Models.ValueConverters;
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class RecordingDetailPageVM : BaseViewModel, IRecordingDetailPageVM
    {
        private static SolidColorBrush invalidColor = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush validColor = new SolidColorBrush(Colors.Transparent);

        private RecordingDTO currentDTO;
        private IRecordingRepository recordingRepo;
        private IProjectRepository projectRepo;
        ITimeStringParser parser;

        public RecordingDetailPageVM(IRecordingRepository recordingRepo, 
            IProjectRepository projectRepo, IParserFieldVMFactory fieldVMFactory)
        {
            this.recordingRepo = recordingRepo;
            this.projectRepo = projectRepo;

            parser = new TimeStringParser();
            var getProjectTask = projectRepo.Read();
            getProjectTask.Wait();
            var projects = new List<ProjectDTO>(getProjectTask.Result);
            ProjectSearchBox = new SearchBoxVM<ProjectDTO>(projects, (dto) => dto.Name);

            var buttonColors = ColorConstants.GetProjectColors().
                Select(c => new ButtonColor { Color = new SolidColorBrush(c) });
            ColorValues = new ObservableCollection<ButtonColor>(buttonColors);
        }

        public SolidColorBrush StartBorderColor { get 
            {
                return IsStartValid ? validColor : invalidColor;
            }
        }

        public SolidColorBrush EndBorderColor {
            get {
                return IsEndValid ? validColor : invalidColor;
            }
        }

        private bool isStartValid;
        public bool IsStartValid {
            get { return isStartValid; }
            set {
                if (value != isStartValid)
                {
                    isStartValid = value;
                    OnPropertyChanged();
                    OnPropertyChanged("BorderColor");
                }
            }
        }

        private bool isEndValid;
        public bool IsEndValid {
            get { return isEndValid; }
            set {
                if (value != isEndValid)
                {
                    isEndValid = value;
                    OnPropertyChanged();
                    OnPropertyChanged("BorderColor");
                }
            }
        }

        private string startText;
        public string StartText {
            get { return startText; }
            set {
                startText = value;
                OnPropertyChanged();
                IsStartValid = parser.TryParse(value, out TimeSpan time);
                if (IsStartValid)
                    StartTime = time;
            }
        }

        private string endText;
        public string EndText {
            get { return endText; }
            set {
                endText = value;
                OnPropertyChanged();
                IsEndValid = parser.TryParse(value, out TimeSpan time);
                if (IsEndValid)
                    EndTime = time;
            }
        }

        private TimeSpan startTime;
        public TimeSpan StartTime {
            get { return startTime; }
            set {
                startTime = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan endTime;
        public TimeSpan EndTime {
            get { return endTime; }
            set {
                endTime = value;
                OnPropertyChanged();
            }
        }

        private string elapsedText;
        public string ElapsedText {
            get { return elapsedText; }
            set {
                elapsedText = value;
                OnPropertyChanged();
            }
        }

        private DateTime startDate;
        public DateTime StartDate {
            get { return startDate; }
            set {
                startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime endDate;
        public DateTime EndDate {
            get { return endDate; }
            set {
                endDate = value;
                OnPropertyChanged();
            }
        }

        private string title;
        public string Title {
            get { return title; }
            set {
                title = value;
                OnPropertyChanged();
            }
        }

        private Visibility createMenuVisibility = Visibility.Collapsed;
        public Visibility CreateMenuVisibility {
            get { return createMenuVisibility; }
            set {
                createMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility chooseMenuVisibility = Visibility.Visible;
        public Visibility ChooseMenuVisibility {
            get { return chooseMenuVisibility; }
            set {
                chooseMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        private string toggleButtonText = "Choose";
        public string ToggleButtonText {
            get { return toggleButtonText; }
            set {
                toggleButtonText = value;
                OnPropertyChanged();
            }
        }

        private Color chosenColor;
        public Color ChosenColor {
            get { return chosenColor; }
            set {
                chosenColor = value;
                OnPropertyChanged("ChosenColor");
            }
        }

        public ICommand ToggleMenuVisibilityCommand {
            get {
                return new RelayCommand(_ =>
               {
                   if (CreateMenuVisibility == Visibility.Collapsed)
                   {
                       CreateMenuVisibility = Visibility.Visible;
                       ChooseMenuVisibility = Visibility.Collapsed;
                       ChooseColorVisibility = Visibility.Collapsed;
                       ToggleButtonText = "Create new";
                   }
                   else
                   {
                       ChooseMenuVisibility = Visibility.Visible;
                       CreateMenuVisibility = Visibility.Collapsed;
                       ChooseColorVisibility = Visibility.Collapsed;
                       ToggleButtonText = "Choose existing";
                   }
               });
            }
        }
        private ObservableCollection<ButtonColor> colorValues;
        public ObservableCollection<ButtonColor> ColorValues {
            get { return colorValues; }
            set {
                colorValues = value;
                OnPropertyChanged();
            }
        }

        public class ButtonColor : BaseViewModel
        {
            private SolidColorBrush color;
            public SolidColorBrush Color {
                get {
                    return color;
                }
                set {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        private SearchBoxVM<ProjectDTO> projectSearchBox;
        public SearchBoxVM<ProjectDTO> ProjectSearchBox {
            get {
                return projectSearchBox;
            }
            set {
                projectSearchBox = value;
                OnPropertyChanged();
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

        public void UpdateFromDTO(int id)
        {
            var dto = recordingRepo.FindAsync(id);
            dto.Wait();
            UpdateFromDTO(dto.Result);
        }

        public void UpdateFromDTO(RecordingDTO recording)
        {
            currentDTO = recording;

            Title = recording.Title;
            StartText = recording.Start.TimeOfDay.ToHHMM();
            StartDate = recording.Start;

            if (recording.End != null)
            {
                TimeSpan? duration = recording.End - recording.Start;
                ElapsedText = duration.Value.ToHHMMSS();

                EndText = recording.End.Value.TimeOfDay.ToHHMM();
                EndDate = recording.End.Value;
            } else
            {
                ElapsedText = (DateTime.Now - recording.Start).ToHHMMSS();
            }
        }

        private DateTime StartDateTime => StartDate + StartTime;
        private DateTime EndDateTime => EndDate + EndTime;
        private bool IsValid()
        {
            return IsStartValid && IsEndValid && (EndDateTime > StartDateTime);
        }

        public bool TrySaveToDTO()
        {
            if (IsValid())
            {
                SaveToDTO();
                return true;
            }
            return false;
        }
        private void SaveToDTO()
        {
            //if (currentDTO == null)
                //currentDTO = new RecordingDTO();
            currentDTO.Title = Title;
            currentDTO.Start = StartDate + StartTime;
            currentDTO.End = EndDate + EndTime;
            currentDTO.Project = Project;
        }
        public ICommand ToggleColorVisibilityCommand {
            get {
                return new RelayCommand(_ =>
                {
                   if (ChooseColorVisibility == Visibility.Collapsed)
                       ChooseColorVisibility = Visibility.Visible;
                   else
                       ChooseColorVisibility = Visibility.Collapsed;
                });
            }
        }

        private Visibility chooseColorVisibility;
        public Visibility ChooseColorVisibility {
            get { return chooseColorVisibility; }
            set {
                chooseColorVisibility = value;
                OnPropertyChanged();
            }
        }

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
