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
using TimeRecorder.Models.Services;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.RepositoryInterfaces;
using TimeRecorder.Models.Services.Strategies;
using TimeRecorder.Models.ValueConverters;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class RecordingOverviewPageVM : BaseViewModel, IRecordingOverviewVM
    {
        private StorageStrategy strategy;
        private IRecordingStrategy recordingStrategy;
        private IProjectStrategy projectStrategy;
        private IRecordingRepository recordingRepo { get =>
                recordingStrategy.CreateRepository(strategy);
        }
        private IProjectRepository projectRepo { get =>
                projectStrategy.CreateRepository(strategy);
        }
        private static readonly string titlePlaceholder = "What are you doing?";
        private static readonly string titleDefault = "No Recording Title";

        public RecordingOverviewPageVM(IRecordingStrategy recordingStrategy,
            IProjectStrategy projectStrategy)
        {
            this.recordingStrategy = recordingStrategy;
            this.projectStrategy = projectStrategy;
            strategy = StorageStrategy.Online;
            Recordings = new ObservableCollection<RecordingSummaryVM>();
            var task = new Task( async () => await LoadItems());
            task.RunSynchronously();
        }

        DispatcherTimer ElapsedUpdater = new DispatcherTimer();
        private async Task LoadItems()
        {
            var recordingDTOs = await recordingRepo.ReadAmount(20, 0);
            var summaryVMs = recordingDTOs.Select(rec => rec.ToSummaryVM()).ToList();
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

        public ICommand RemoveEntryCommand {
            get {
                return new RelayCommand(async (rec) =>
                {
                    var item = (RecordingSummaryVM)rec;
                    var success = await recordingRepo.DeleteAsync(item.Id);
                    if (success)
                        Recordings.Remove(item);
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
                return new RelayCommand(async _ =>
                {
                    if (ToggleButtonText == "Start")
                        await startNewTimer();
                    else
                        await StopTimer();
                });
            }
        }

        private async Task startNewTimer()
        {
            if (Title == titlePlaceholder || Title == "")
                Title = titleDefault;
            await StartTimer();
        }
        public ICommand ContinueRecordingCommand {
            get {
                return new RelayCommand(async listIndex =>
                {
                    if (!(listIndex is int))
                        throw new ArgumentException("ContinueRecordingButton - argument was not an int.");
                    var oldRecordingSummary = Recordings[(int)listIndex];
                    var oldRecording = await recordingRepo.FindAsync(oldRecordingSummary.Id);
                    if (oldRecording == null)
                        throw new Exception("Recording not found");
                    Title = oldRecording.Title;
                    await StartTimer(oldRecording.ProjectId);
                });
            }
        }

        private async Task StartTimer(int? projectId = default(int?))
        {
            startTime = DateTime.Now;
            ToggleButtonText = "Stop";

            currentRecording = new RecordingDTO
            {
                Start = startTime,
                Title = Title,
                ProjectId = projectId,
            };

            ElapsedUpdater.Tick += new EventHandler(TickHandler);
            ElapsedUpdater.Interval = TimeSpan.FromMilliseconds(100);
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
            Title = "";
            currentRecording = null;

        }

        private void TickHandler(object sender, EventArgs e)
        {
            Elapsed = DateTime.Now - startTime;
        }
    }
}
