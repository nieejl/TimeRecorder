using Microsoft.EntityFrameworkCore;
using Server.RepositoryLayer.Models.Entities;
using System.Diagnostics;
using System.IO;

namespace Server.RepositoryLayer.Models
{
    public class TimeRecorderServerContext : DbContext, ITimeRecorderServerContext
    {
        DbSet<Recording> Recordings { get; set; }
        DbSet<Project> Projects { get; set; }

        public TimeRecorderServerContext(DbContextOptions options) : base(options)
        {

        }

        public TimeRecorderServerContext() : base()
        {
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                //builder.UseSqlite("Filename=./SampleServerDB");
                builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TimeRecorder;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Recording>().HasIndex(r => r.TemporaryId);
            builder.Entity<Project>().HasIndex(p => p.TemporaryId);
        }
    }
}
