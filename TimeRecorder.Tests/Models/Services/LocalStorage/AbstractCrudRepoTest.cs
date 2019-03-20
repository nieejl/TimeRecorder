using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.LocalStorage;
using Xunit;

namespace TimeRecorder.Tests.Models.Services.LocalStorage
{
    public abstract class AbstractCrudRepoTest<EntityType, RepoType>
        where RepoType : AbstractCrudRepo<EntityType>
        where EntityType : LocalEntity
    {

        public Mock<ITimeRecorderContext> CreateMock()
        {
            return new Mock<ITimeRecorderContext>();
        }
        public Mock<DbSet<EntityType>> CreateDbSetFindMock(bool found) {
            var mock = new Mock<DbSet<EntityType>>();
            if (found)
            {
                var entity = CreateSampleValue();
                mock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
                //mock.Setup(m => m.Find(It.IsAny<object[]>())).Returns(entity);
                //mock.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(entity);
                return mock;
            }
            mock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(default(EntityType));
            return mock;
        }
        public abstract void CopyToFrom(EntityType to, EntityType from);
        public abstract EntityType CreateSampleValue(int id = 1);
        public abstract EntityType CreateDifferentSampleValue(int id = 1);
        public EntityType CreateSampleNullValue() => null;
        public abstract RepoType CreateRepo(ITimeRecorderContext context);
        public RepoType CreateInMemoryRepo()
        {
            var guid = new Guid();
            var builder = new DbContextOptionsBuilder<TimeRecorderContext>();
            builder.UseInMemoryDatabase(guid.ToString());
            var context = new TimeRecorderContext(builder.Options);
            return CreateRepo(context);
        }
        //public abstract IQueryable<EntityType> GetQueryable();

        #region CreateAsync-tests
        [Fact(DisplayName = "CreateAsync given entity returns id")]
        public async Task Test_CreateAsync_Returns_Id()
        {
            // Arrange
            var repo = CreateInMemoryRepo();
            int id = 8094;
            var entity = CreateSampleValue(id);
            // Act
            var result = await repo.CreateAsync(entity);
            // Assert
            Assert.Equal(id, result);
        }

        #endregion

        #region DeleteAsync-Tests
        [Fact(DisplayName = "DeleteAsync removes the correct item")]
        public async Task Test_DeleteAsync_Removes_The_Correct_Item()
        {
            // Arrange
            var repo = CreateInMemoryRepo();

            var ids = new List<int>();
            ids.Add(await repo.CreateAsync(CreateSampleValue(700)));
            ids.Add(await repo.CreateAsync(CreateSampleValue(701)));
            ids.Add(await repo.CreateAsync(CreateSampleValue(702)));
            int toRemove = ids[0];
            // Act
            var result = await repo.DeleteAsync(toRemove);
            // Assert
            Assert.True(result);
            Assert.Null(await repo.FindAsync(ids[0]));
            Assert.Equal(701, (await repo.FindAsync(ids[1])).Id);
            Assert.Equal(702, (await repo.FindAsync(ids[2])).Id);
        }

        [Fact(DisplayName = "DeleteAsync returns false if item not found")]
        public async Task Test_Delete_Return_False_If_Item_Not_Found()
        {
            // Arrange
            var repo = CreateInMemoryRepo();

            var result = await repo.DeleteAsync(2048);

            Assert.False(result);
        }

        #endregion

        #region FindAsync-Tests

        [Fact(DisplayName = "FindAsync returns entity if id found")]
        public async Task Test_FindAsync_Returns_Entity_If_Id_Found()
        {
            var repo = CreateInMemoryRepo();
            var entity = CreateSampleValue(987);

            int id = await repo.CreateAsync(entity);
            var result = await repo.FindAsync(id);

            Assert.IsType<EntityType>(result);
            Assert.Equal(id, result.Id);
        }
        #endregion

        #region UpdateAsync-Tests
        [Fact(DisplayName = "UpdateAsync throws exception if item not found.")]
        public async Task Test_UpdateAsync_Returns_False_If_Item_Not_Found()
        {
            var repo = CreateInMemoryRepo();
            var entity = CreateSampleValue(24);
            await repo.CreateAsync(entity);
            var newEntity = CreateSampleValue(25);

            var result = await repo.UpdateAsync(newEntity);

            Assert.False(result);
        }

        [Fact(DisplayName = "UpdateAsync updates item if found.")]
        public async Task Test_UpdateAsync_Updates_Item_If_Found()
        {
            var repo = CreateInMemoryRepo();
            var oldEntity = CreateSampleValue(66);
            oldEntity.Id = await repo.CreateAsync(oldEntity);
            var newEntity = CreateDifferentSampleValue(66);

            CopyToFrom(oldEntity, newEntity);
            var updated = await repo.UpdateAsync(oldEntity);
            var result = await repo.FindAsync(oldEntity.Id);

            Assert.True(updated);
            Assert.Equal(newEntity, result);
        }

        #endregion

        #region Calls-SaveChanges
        [Fact(DisplayName = "UpdateAsync calls savechanges if item found.")]
        public async Task Test_UpdateAsync_Calls_Update_And_SaveChanges_If_Entity_Found()
        {
            var context = CreateMock();
            var dbset = CreateDbSetFindMock(found:true);
            var entity = CreateSampleValue();

            dbset.Setup(m => m.Update(It.IsAny<EntityType>())).Verifiable();
            context.Setup(m => m.Set<EntityType>()).Returns(dbset.Object);
            context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var repo = CreateRepo(context.Object);

            var result = await repo.UpdateAsync(entity);

            dbset.Verify(m => m.Update(It.IsAny<EntityType>()), Times.Once);
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
            Assert.True(result);
        }

        [Fact(DisplayName = "DeleteAsync calls savechanges if item found.")]
        public async Task Test_DeleteAsync_Calls_Remove_And_SaveChanges_If_Entity_Found()
        {
            var entity = CreateSampleValue();
            var context = CreateMock();
            var dbset = CreateDbSetFindMock(found:true);

            dbset.Setup(m => m.Remove(It.IsAny<EntityType>())).Verifiable();
            context.Setup(m => m.Set<EntityType>()).Returns(dbset.Object);
            context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var repo = CreateRepo(context.Object);

            var result = await repo.DeleteAsync(entity.Id);

            dbset.Verify(m => m.Remove(It.IsAny<EntityType>()), Times.Once);
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
            Assert.True(result);
        }

        [Fact(DisplayName = "CreateAsync calls savechanges if entity is not null")]
        public async Task Test_CreateAsync_Calls_SaveChanges_If_Entity_Is_Not_Null()
        {
            var context = CreateMock();
            var dbset = CreateDbSetFindMock(found: true);
            dbset.Setup(m => m.Add(It.IsAny<EntityType>())).Verifiable();
            context.Setup(m => m.Set<EntityType>()).Returns(dbset.Object);
            context.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);
            var repo = CreateRepo(context.Object);
            var entity = CreateSampleValue(1);

            var result = await repo.CreateAsync(entity);

            dbset.Verify(m => m.Add(It.IsAny<EntityType>()), Times.Once);
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact(DisplayName = "UpdateAsync does not call savechanges if item not found.")]
        public async Task Test_UpdateAsync_Doesnt_Call_Update_If_Entity_Not_Found()
        {
            var context = CreateMock();
            var dbset = CreateDbSetFindMock(found: false);
            var entity = CreateSampleValue();

            dbset.Setup(m => m.Update(It.IsAny<EntityType>())).Verifiable();
            context.Setup(m => m.Set<EntityType>()).Returns(dbset.Object);
            context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var repo = CreateRepo(context.Object);

            var result = await repo.UpdateAsync(entity);

            dbset.Verify(m => m.Update(It.IsAny<EntityType>()), Times.Never);
            context.Verify(m => m.SaveChangesAsync(), Times.Never);
            Assert.False(result);
        }

        [Fact(DisplayName = "DeleteAsync does not call savechanges if item not found.")]
        public async Task Test_DeleteAsync_Doesnt_Call_SaveChanges_If_Entity_Not_Found()
        {
            var context = CreateMock();
            var dbset = CreateDbSetFindMock(found:false);
            dbset.Setup(m => m.Remove(It.IsAny<EntityType>())).Verifiable();
            context.Setup(m => m.Set<EntityType>()).Returns(dbset.Object);
            context.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);

            var repo = CreateRepo(context.Object);

            var result = await repo.DeleteAsync(1);

            dbset.Verify(m => m.Remove(It.IsAny<EntityType>()), Times.Never);
            context.Verify(m => m.SaveChangesAsync(), Times.Never);
            Assert.False(result);
        }

        [Fact(DisplayName = "CreateAsync does not call savechanges if item is null")]
        public async Task Test_CreateAsync_Doesnt_Call_SaveChanges_If_Entity_Is_Null()
        {
            var context = CreateMock();
            var dbset = CreateDbSetFindMock(found: false);
            dbset.Setup(m => m.AddAsync(It.IsAny<EntityType>(), It.IsAny<CancellationToken>())).Verifiable();
            context.Setup(m => m.Set<EntityType>()).Returns(dbset.Object);
            context.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);

            var repo = CreateRepo(context.Object);

            var result = await repo.CreateAsync(null);

            dbset.Verify(m => m.AddAsync(It.IsAny<EntityType>(), default(CancellationToken)), Times.Never);
            context.Verify(m => m.SaveChangesAsync(), Times.Never);
        }

        #endregion
    }
}
