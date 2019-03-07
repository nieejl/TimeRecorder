using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
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

        public void FlipButtonText()
        {
            StartButtonText = StartButtonText == "Start" ? "Stop" : "Start";
        }

        public void StartTimer()
        {
            Elapsed = DateTime.Now;
        }

    }
}
