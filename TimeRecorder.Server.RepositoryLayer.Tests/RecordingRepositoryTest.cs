using Microsoft.EntityFrameworkCore;
using System;
using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using Xunit;

namespace TimeRecorder.Server.RepositoryLayer.Tests
{
    public class RecordingRepositoryTest
    {
        #region Dummy-classes
        public class DummyContext : DbContext, ITimeRecorderServerContext
        {
            public DbSet<Dummy> Dummies { get; set; }
            public DummyContext(DbContextOptions options) : base(options)
            {
            }
        }
        public class Dummy : Entity
        {
            public int DummyVariable { get; set; }
        }
        public class DummyRepo : AbstractCrudServerRepo<Dummy>
        {
            public DummyRepo(ITimeRecorderServerContext context) : base(context)
            {
            }
        }
        public DummyRepo CreateNewDummyRepo(string dbName)
        {
            var builder = new DbContextOptionsBuilder<DummyContext>();
            builder.UseInMemoryDatabase(dbName);
            var context = new DummyContext(builder.Options);
            return new DummyRepo(context);
        }

        #endregion

        [Fact]
        public void doSomething()
        {
        }
    }
}
