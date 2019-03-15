using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.LocalStorage;
using Xunit;

namespace TimeRecorder.Tests.Models.Repositories
{
    public class AbstractCrudRepoTest
    {
        public class AbstractRepoTest
        {
            #region Dummy-classes
            public class DummyContext : DbContext, ITimeRecorcerContext
            {
                public DbSet<Dummy> Dummies { get; set; }
                public DummyContext(DbContextOptions options) : base(options)
                {
                }
            }
            public class Dummy : LocalEntity
            {
                public int Id { get; set; }
                public int DummyVariable { get; set; }
            }
            public class DummyRepo : AbstractCrudRepo<Dummy>
            {
                public DummyRepo(ITimeRecorcerContext context) : base(context)
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

            #region CreateAsync-tests
            [Fact(DisplayName = "CreateAsync given entity returns id")]
            public async Task Test_CreateAsync_Returns_Id()
            {
                // Arrange
                var repo = CreateNewDummyRepo(nameof(Test_CreateAsync_Returns_Id));
                var dummy = new Dummy { DummyVariable = 22 };
                // Act
                var result = await repo.CreateAsync(dummy);
                // Assert
                Assert.Equal(1, result);
            }

            #endregion

            #region DeleteAsync-Tests
            [Fact(DisplayName = "DeleteAsync removes the correct item")]
            public async Task Test_deleteasync_removes_the_correct_item()
            {
                // Arrange
                var repo = CreateNewDummyRepo("123");

                var ids = new List<int>();
                ids.Add(await repo.CreateAsync(new Dummy { DummyVariable = 2 }));
                ids.Add(await repo.CreateAsync(new Dummy { DummyVariable = 3 }));
                ids.Add(await repo.CreateAsync(new Dummy { DummyVariable = 4 }));
                int toRemove = ids[0];
                // Act
                var result = await repo.DeleteAsync(toRemove);
                // Assert
                Assert.True(result);
                Assert.Null(await repo.FindAsync(ids[0]));
                Assert.Equal(3, (await repo.FindAsync(ids[1])).DummyVariable);
                Assert.Equal(4, (await repo.FindAsync(ids[2])).DummyVariable);
            }

            [Fact(DisplayName = "DeleteAsync returns false if item not found")]
            public async Task Test_delete_return_false_if_item_not_found()
            {
                // Arrange
                var repo = CreateNewDummyRepo("345");

                var result = await repo.DeleteAsync(2048);

                Assert.False(result);
            }

            #endregion

            #region FindAsync-Tests

            [Fact(DisplayName = "FindAsync returns entity if id found")]
            public async Task Test_findasync_returns_entity_if_id_found()
            {
                var repo = CreateNewDummyRepo("231");

                int id = await repo.CreateAsync(new Dummy { DummyVariable = 42 });
                var result = await repo.FindAsync(id);

                Assert.IsType<Dummy>(result);
                Assert.Equal(id, result.Id);
            }
            #endregion

            #region UpdateAsync-Tests
            [Fact(DisplayName = "UpdateAsync throws exception if item not found.")]
            public async Task Test_update_returns_false_if_item_not_found()
            {
                var repo = CreateNewDummyRepo("76453");
                var oldDummy = new Dummy { DummyVariable = 1 };
                await repo.CreateAsync(oldDummy);
                var newDummy = new Dummy { DummyVariable = 42 };

                var result = await repo.UpdateAsync(newDummy);

                Assert.False(result);
            }

            [Fact(DisplayName = "UpdateAsync updates item if found.")]
            public async Task Test_update_updates_item_if_found()
            {
                var repo = CreateNewDummyRepo("safga");
                var oldDummy = new Dummy { DummyVariable = 1 };
                oldDummy.Id = await repo.CreateAsync(oldDummy);

                oldDummy.DummyVariable = 42;
                var result = await repo.UpdateAsync(oldDummy);

                Assert.True(result);
                Assert.Equal(42, (await repo.FindAsync(oldDummy.Id)).DummyVariable);
            }


            #endregion
        }
    }
}
