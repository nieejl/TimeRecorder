using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.DTOs
{
    public class RecordingDTO
    {
        TimeSpan StartTime { get; set; }
        TimeSpan EndTime { get; set; }

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        ProjectDTO Project { get; set; }
    }
}
