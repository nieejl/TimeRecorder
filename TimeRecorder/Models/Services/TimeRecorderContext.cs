using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.Repositories
{
    public class TimeRecorderContext : DbContext, ITimeRecorcerContext
    {
        DbSet<RecordingDTO> Recordings { get; set; }
        DbSet<ProjectDTO> Projects { get; set; }

        public TimeRecorderContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlite("Filename=./SampleDB");
            }
        }
    }
}
