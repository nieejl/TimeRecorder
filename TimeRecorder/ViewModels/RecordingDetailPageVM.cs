using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TimeRecorder.Models;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.RepositoryInterfaces;
using TimeRecorder.Models.Services.Strategies;
using TimeRecorder.Models.ValueConverters;
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class RecordingDetailPageVM : BaseViewModel, IRecordingDetailPageVM
    {
        private static SolidColorBrush invalidColor = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush validColor = new SolidColorBrush(Colors.Transparent);
        private StorageStrategy strategy;
        private DispatcherTimer elapsedTimer;

        private RecordingDTO currentDTO;
        private IRecordingStrategy recordingStrategy;
        private IProjectStrategy projectStrategy;
        private IRecordingRepository recordingRepo { get =>
                recordingStrategy.CreateRepository(strategy); }
        private IProjectRepository projectRepo { get =>
            projectStrategy.CreateRepository(strategy);
        }

        ITimeStringParser parser;

        //public RecordingDetailPageVM(IRecordingRepository recordingRepo, 
        //    IProjectRepository projectRepo, IParserFieldVMFactory fieldVMFactory)
        public RecordingDetailPageVM(IRecordingStrategy recordingStrategy, 
            IProjectStrategy projectStrategy)
        {
            this.recordingStrategy = recordingStrategy;
            this.projectStrategy = projectStrategy;
            strategy = StorageStrategy.Online;
            Task.Run( () => LoadItems());

            parser = new TimeStringParser();
            var buttonColors = ColorConstants.GetProjectColors().
                Select(c => new ButtonColor { Color = new SolidColorBrush(c) });
            ColorValues = new ObservableCollection<ButtonColor>(buttonColors);

            elapsedTimer = new DispatcherTimer();
            elapsedTimer.Interval = TimeSpan.FromMilliseconds(100);
            elapsedTimer.Tick += updateElapsedTick;
        }

        async Task LoadItems()
        {
            var projectTask = await projectRepo.Read();
            var projects = new List<ProjectDTO>(projectTask);
            ProjectSearchBox = new SearchBoxVM<ProjectDTO>(projects, (dto) => dto.Name);
        }

        public SolidColorBrush StartBorderColor { get 
            {
                return GetValidityColor(IsStartValid);
            }
        }

        public SolidColorBrush EndBorderColor {
            get {
                return GetValidityColor(IsEndValid);
            }
        }

        public SolidColorBrush ElapsedBorderColor {
            get {
                return GetValidityColor(IsElapsedValid);
            }
        }

        public SolidColorBrush GetValidityColor(bool valid)
        {
            return valid ? validColor : invalidColor;
        }

        private bool isStartValid;
        public bool IsStartValid {
            get { return isStartValid; }
            set {
                if (value != isStartValid)
                {
                    isStartValid = value;
                    OnPropertyChanged();
                    OnPropertyChanged("StartBorderColor");
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
                    OnPropertyChanged("EndBorderColor");
                }
            }
        }

        private bool isElapsedValid;
        public bool IsElapsedValid {
            get { return isElapsedValid; }
            set {
                if (value != isElapsedValid)
                {
                    isElapsedValid = value;
                    OnPropertyChanged();
                    OnPropertyChanged("ElapsedBorderColor");
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
                {
                    EndTime = time;
                    stopElapsedUpdater();
                    updateElapsed();
                }
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
                IsElapsedValid = parser.TryParse(elapsedText, out TimeSpan time);
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
                if (IsEndValid)
                {
                    stopElapsedUpdater();
                    updateElapsed();
                }
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

        private string toggleButtonText = "Choose existing";
        public string ToggleButtonText {
            get { return toggleButtonText; }
            set {
                toggleButtonText = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush chosenColor;
        public SolidColorBrush ChosenColor {
            get { return chosenColor; }
            set {
                chosenColor = value;
                OnPropertyChanged();
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

        private Visibility chooseColorVisibility = Visibility.Collapsed;
        public Visibility ChooseColorVisibility {
            get { return chooseColorVisibility; }
            set { 
                chooseColorVisibility = value;
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
                if (value != null)
                {
                    ChosenColor = new SolidColorBrush(value.Color);
                    ProjectSearchBox.SearchText = value.Name;
                }
                OnPropertyChanged("Project");
            }
        }

        public void UpdateFromDTO(int id)
        {
            Debug.WriteLine("ID we're looking for: " + id);
            var task = recordingRepo.FindAsync(id);
            Task.Run(async () =>
            {
                var dto = await recordingRepo.FindAsync(id);
                UpdateFromDTO(dto);
            });
        }

        public void UpdateFromDTO(RecordingDTO recording)
        {
            currentDTO = recording ?? throw new ArgumentNullException("RecordingDTO was null");

            Title = recording.Title;
            if (recording.Start.TimeOfDay.Seconds == 0)
                StartText = recording.Start.TimeOfDay.ToHHMM();
            else
                StartText = recording.Start.TimeOfDay.ToHHMMSS();
            StartTime = recording.Start.TimeOfDay;
            StartDate = recording.Start.Date;

            if (recording.Project != null)
            {
                Project = recording.Project;
                ChosenColor = new SolidColorBrush(project.Color);
            }
            if (recording.End != null)
            {
                TimeSpan? duration = recording.End - recording.Start;
                ElapsedText = duration.Value.ToHHMMSS();
                if (recording.Start.TimeOfDay.Seconds == 0)
                    EndText = recording.End.Value.TimeOfDay.ToHHMM();
                else
                    EndText = recording.End.Value.TimeOfDay.ToHHMMSS();
                EndDate = recording.End.Value.Date;
            } else
            {
                startElapsedUpdater();
            }
        }

        private void startElapsedUpdater()
        {
            elapsedTimer.Start();
        }
        private void stopElapsedUpdater()
        {
            if (elapsedTimer != null)
                elapsedTimer.Stop();
        }

        private void updateElapsedTick(object sender, EventArgs e)
        {
            updateElapsed();
        }
        private void updateElapsed()
        {
            if (!IsEndValid)
                ElapsedText = (DateTime.Now - startDateTime).ToHHMMSS();
            else
                ElapsedText = (endDateTime - startDateTime).ToHHMMSS();
        }

        private DateTime startDateTime => StartDate + StartTime;
        private DateTime endDateTime => EndDate + EndTime;
        private bool IsValid()
        {
            return IsStartValid && IsEndValid && (endDateTime > startDateTime);
        }

        public ICommand SaveCommand {
            get {
                return new RelayCommand(_ =>
                {
                    currentDTO.Title = Title;
                    currentDTO.Start = StartDate + StartTime;
                    currentDTO.Project = ProjectSearchBox.AutoCompleteItem;
                    if (IsValid())
                        currentDTO.End = EndDate + EndTime;
                    recordingRepo.UpdateAsync(currentDTO);
               });
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
                       ToggleButtonText = "Choose existing";
                   }
                   else
                   {
                       ChooseMenuVisibility = Visibility.Visible;
                       CreateMenuVisibility = Visibility.Collapsed;
                       ChooseColorVisibility = Visibility.Collapsed;
                       ToggleButtonText = "Create new";
                   }
               });
            }
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

        public ICommand AddNewProjectCommand {
            get {
                return new RelayCommand( async (name) =>
                {
                    var nameString = (string)name;
                    var Color = ChosenColor.Color;
                    var project = new ProjectDTO
                    {
                        Name = nameString,
                        Color = Color
                    };
                    await projectRepo.CreateAsync(project);
                    ToggleMenuVisibilityCommand.Execute(null);
                    Project = project;
                });
            }
        }

        public void ChooseColor(SolidColorBrush color)
        {
            var previous = ChosenColor == null ? color : color;
            ChosenColor = color;
            var button = ColorValues.First(bc => bc.Color.Color.Equals(color.Color));
            int index = ColorValues.IndexOf(button);
            ColorValues[index] = new ButtonColor { Color = previous };
        }
    }
}
