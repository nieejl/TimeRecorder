using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;

namespace TimeRecorder.Models
{
    public class Timer
    {
        private RecordingDTO dto;
        private string elapsed;
        private DispatcherTimer elapsedTimer;
        DateTime start;
        DateTime end;
        bool active;

        public Timer(RecordingDTO dto)
        {
            this.dto = dto;
        }
        public bool IsActive()
        {
            return active;
        }
        public TimeSpan GetElapsed()
        {
            if (IsActive())
                return DateTime.Now - start;
            else
                return end - start;
        }

        public void KeepElapsedUpdated(ref string target, DateTime start)
        {
            active = true;
            this.start = start;
            target = elapsed;
            elapsedTimer = new DispatcherTimer();
            elapsedTimer.Interval = TimeSpan.FromMilliseconds(100);
            elapsedTimer.Tick += updateElapsedTick;
        }

        private void updateElapsedTick(object sender, EventArgs e)
        {
            updateElapsed();
        }
        private void updateElapsed()
        {
            elapsed = (DateTime.Now - start).ToHHMMSS();
        }

        public void StopUpdating()
        {
            end = DateTime.Now;
            elapsedTimer.Stop();
            active = false;
        }
    }
}
