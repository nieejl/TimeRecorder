using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TimeRecorder.Models;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class RecordingOverviewVM : BaseViewModel, IRecordingOverviewVM
    {
        public RecordingOverviewVM()
        {
            ExecuteLoadItemsCommand().Wait();
        }

        private DateTime elapsed;
        public DateTime Elapsed {
            get { return elapsed; }
            set {
                elapsed = value;
                OnPropertyChanged();
            }
        }
        private string startButtonText = "Start";
        public string StartButtonText {
            get { return startButtonText; }
            set {
                startButtonText = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Recording> recordings;
        public ObservableCollection<Recording> Recordings {
            get { return recordings; }
            set {
                recordings = value;
                OnPropertyChanged();
            }
        }

        private async Task ExecuteLoadItemsCommand()
        {
            Projects = new ObservableCollection<Project>();
            Recordings = new ObservableCollection<Recording>();
            await Task.FromResult(0);
        }

        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects {
            get { return projects; }
            set {
                projects = value;
                OnPropertyChanged();
            }
        }
        public class Recording : BaseViewModel
        {
            private string projectName;
            public string ProjectName {
                get { return projectName; }
                set {
                    projectName = value;
                    OnPropertyChanged();
                }
            }
            private string description;
            public string Description {
                get { return description; }
                set {
                    description = value;
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
            private Project project;
            public Project Project {
                get { return project; }
                set {
                    project = value;
                    OnPropertyChanged();
                }
            }
        }

        public class Project : BaseViewModel
        {
            private string name = "Hello, world!";
            public string Name {
                get { return name; }
                set {
                    name = value;
                    OnPropertyChanged();
                }
            }
            private SolidColorBrush color = new SolidColorBrush(Colors.AliceBlue);
            public SolidColorBrush Color {
                get { return color; }
                set {
                    color = value;
                    OnPropertyChanged();
                }
            }

        }

        public ICommand RemoveEntryCommand {
            get {
                return new RelayCommand((rec) =>
                {
                    var item = (Recording)rec;
                    Debug.WriteLine(item.ProjectName);
                });
            }
        }

        public ICommand LoadMoreCommand {
            get {
                return new RelayCommand((item) =>
                {
                    Debug.WriteLine("LoadMoreCommand called");
                });
            }
        }

        public ICommand ToggleTimerCommand {
            get {
                return new RelayCommand((item) =>
                {
                    ToggleTimer();
                });
            }
        }
        private void ToggleTimer()
        {
            Elapsed = DateTime.Now;
            StartButtonText = "Stop";
            var project = new Project()
            {
                Name = Projects.Count.ToString(),
                Color = new SolidColorBrush(Colors.Brown)
            };
            Projects.Add(project);

            Recordings.Add(new Recording() {
                ProjectName = Recordings.Count.ToString(),
                Description = (2*Recordings.Count).ToString(),
                Duration = DateTime.Now.ToShortTimeString(),
                Project = project
            });
            if (Projects.Count > 3) Projects[2].Color = new SolidColorBrush(Colors.Pink);
            Debug.WriteLine(Recordings.Count + ",  " + Projects.Count);
        }

    }
}
