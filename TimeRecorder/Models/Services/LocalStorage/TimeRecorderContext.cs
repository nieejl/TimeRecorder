using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public class TimeRecorderContext : DbContext, ITimeRecorderContext
    {
        DbSet<RecordingDTO> RecordingDTOs { get; set; }
        DbSet<ProjectDTO> ProjectDTOs { get; set; }
        DbSet<TagDTO> TagDTOs { get; set; }

        public TimeRecorderContext(DbContextOptions options) : base(options)
        {

        }

        public TimeRecorderContext() : base()
        {
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

            builder.Entity<RecordingDTO>().HasIndex(r => r.TemporaryId).IsUnique();
            builder.Entity<ProjectDTO>().HasIndex(p => p.TemporaryId).IsUnique();

        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
