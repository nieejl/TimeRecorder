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
        DbSet<RecordingDTO> RecordingDTOs { get; set; }
        DbSet<ProjectDTO> ProjectDTOs { get; set; }
        DbSet<TagDTO> TagDTOs { get; set; }

        public TimeRecorderContext(DbContextOptions options) : base(options)
        {

        }

        public TimeRecorderContext() : base()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlite("Filename=./SampleDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TagDTO>().HasIndex(t => t.TagValue).IsUnique();


        }
    }
}
