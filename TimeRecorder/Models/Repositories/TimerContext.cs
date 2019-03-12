using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Repositories
{
    public class TimerContext : DbContext, ITimerContext
    {
        DbSet<RecordingDTO> Recordings { get; set; }
        DbSet<ProjectDTO> Projects { get; set; }
    }
}
