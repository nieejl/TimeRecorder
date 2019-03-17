using Microsoft.EntityFrameworkCore;
using Server.RepositoryLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlite("Filename=./SampleServerDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Recording>().HasIndex(r => r.TemporaryId);
            builder.Entity<Project>().HasIndex(p => p.TemporaryId);
        }
    }
}
