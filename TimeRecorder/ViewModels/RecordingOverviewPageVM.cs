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
using System.Windows.Threading;
using TimeRecorder.Models;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services.Repositories;
using TimeRecorder.Models.ValueConverters;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class RecordingOverviewPageVM : BaseViewModel, IRecordingOverviewVM
    {
        private IRecordingRepository recordingRepo;
        private IProjectRepository projectRepo;
        private static readonly string titlePlaceholder = "What are you doing?";
        private static readonly string titleDefault = "No Recording Title";

        public RecordingOverviewPageVM(IRecordingRepository recordingRepo, 
            IProjectRepository projectRepo)
        {
            this.recordingRepo = recordingRepo;
            this.projectRepo = projectRepo;
            ExecuteLoadItemsCommand().Wait();
        }

        DispatcherTimer ElapsedUpdater = new DispatcherTimer();
        private async Task ExecuteLoadItemsCommand()
        {
            Projects = new ObservableCollection<ProjectDTO>(await projectRepo.Read());
            var recordingDTOs = await recordingRepo.ReadAmount(20,0);
            var summaryVMs = recordingDTOs.Select(r => r.ToSummaryVM()).ToList();

            Recordings = new ObservableCollection<RecordingSummaryVM>(summaryVMs);
        }
        private DateTime startTime;
        private RecordingDTO currentRecording;


        private TimeSpan elapsed;
        public TimeSpan Elapsed {
            get { return elapsed; }
            set {
                elapsed = value;
                OnPropertyChanged();
                OnPropertyChanged("ElapsedText");
            }
        }
        public string ElapsedText {
            get {
                return Elapsed.ToSimpleString();
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

        private string startButtonText = "Start";
        public string ToggleButtonText {
            get { return startButtonText; }
            set {
                startButtonText = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<RecordingSummaryVM> recordings;
        public ObservableCollection<RecordingSummaryVM> Recordings {
            get { return recordings; }
            set {
                recordings = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ProjectDTO> projects;
        public ObservableCollection<ProjectDTO> Projects {
            get { return projects; }
            set {
                projects = value;
                OnPropertyChanged();
            }
        }

        public ICommand RemoveEntryCommand {
            get {
                return new RelayCommand((rec) =>
                {
                    var item = (RecordingDTO)rec;
                    //Debug.WriteLine(item.Title);
                });
            }
        }

        public ICommand LoadMoreCommand {
            get {
                return new RelayCommand(async (item) =>
                {
                    var recordingDTOs = await recordingRepo.ReadAmount(20, Recordings.Count);
                    var summaryVMs = recordingDTOs.Select(r => r.ToSummaryVM()).ToList();
                    Recordings = new ObservableCollection<RecordingSummaryVM>(
                        Recordings.Concat(summaryVMs));
                });
            }
        }

        public ICommand ToggleTimerCommand {
            get {
                return new RelayCommand(_ => ToggleTimer());
            }
        }
        private void ToggleTimer()
        {
            Task action;
            if (ToggleButtonText == "Start")
            {
                action = StartTimer();
            } else
            {
                action = StopTimer();
            }
            action.Wait();
        }
        private async Task StartTimer()
        {
            if (Title == titlePlaceholder || Title == "")
                Title = titleDefault;
            ToggleButtonText = "Stop";
            startTime = DateTime.Now;
            currentRecording = new RecordingDTO
            {
                Start = startTime,
                Title = Title,
            };

            ElapsedUpdater.Tick += new EventHandler(TickHandler);
            ElapsedUpdater.Interval = new TimeSpan(0,0,0,0,100);
            ElapsedUpdater.Start();
            await recordingRepo.CreateAsync(currentRecording);
        }

        private async Task StopTimer()
        {
            ToggleButtonText = "Start";
            ElapsedUpdater.Tick -= TickHandler;
            ElapsedUpdater.Stop();

            currentRecording.End = DateTime.Now;
            currentRecording.Title = Title;
            await recordingRepo.UpdateAsync(currentRecording);
            var newRecordings = new List<RecordingSummaryVM>();

            newRecordings.Add(currentRecording.ToSummaryVM());
            newRecordings.AddRange(Recordings);
            Recordings = new ObservableCollection<RecordingSummaryVM>(newRecordings);

            clearCurrentTimer();
        }

        private void clearCurrentTimer()
        {
            Elapsed = new TimeSpan(0);
            currentRecording = null;

        }

        private void TickHandler(object sender, EventArgs e)
        {
            Elapsed = DateTime.Now - startTime;
        }
    }
}
