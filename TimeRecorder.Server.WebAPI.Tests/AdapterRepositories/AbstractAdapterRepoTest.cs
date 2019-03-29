using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using TimeRecorder.Server.WebAPI.AdapterRepositories;
using TimeRecorder.Server.WebAPI.Adapters;
using TimeRecorder.Shared;
using Xunit;

namespace TimeRecorder.Server.WebAPI.Tests.AdapterRepositories
{
    public abstract class AbstractAdapterRepoTest<AdapterRepoType, RepoType, DTOType, EntityType>
        where AdapterRepoType : AbstractAdapterRepo<DTOType, EntityType>
        where RepoType : class, IRepository<EntityType>
        where DTOType : DTO
        where EntityType : Entity
    {
        public abstract DTOType CreateSampleDTO();
        public abstract List<DTOType> CreateThreeSampleDTOs();
        public abstract EntityType CreateSampleEntity();
        public abstract List<EntityType> CreateThreeSampleEntities();
        public abstract Mock<IAdapter<DTOType, EntityType>> CreateAdapterMock();
        public abstract Mock<RepoType> CreateRepoMock();
        public abstract AdapterRepoType CreateAdapterRepo(
            RepoType repo, IAdapter<DTOType, EntityType> adapter);

        [Fact]
        public async Task CreateAsync_Calls_Create_On_Repo_Entity_Returns_Id()
        {
            var dto = CreateSampleDTO();
            var entity = CreateSampleEntity();
            entity.Id = 42;
            var adapter = CreateAdapterMock();
            var repo = CreateRepoMock();
            adapter.Setup(m => m.ConvertToEntity(It.IsAny<DTOType>())).Returns(entity);
            repo.Setup(m => m.CreateAsync(It.IsAny<EntityType>())).ReturnsAsync(42);
            var adapterRepo = CreateAdapterRepo(repo.Object, adapter.Object);


            var result = await adapterRepo.CreateAsync(dto);

            repo.Verify(m => m.CreateAsync(It.IsAny<EntityType>()), Times.Once());
            Assert.Equal(42, result);
        }

        [Fact]
        public async Task CreateAsync_Calls_ConvertToEntity_With_Equal_Entity()
        {
            var dto = CreateSampleDTO();
            dto.Id = 42;
            var entity = CreateSampleEntity();
            entity.Id = 42;
            var adapter = CreateAdapterMock();
            var repo = CreateRepoMock();
            adapter.Setup(m => m.ConvertToEntity(It.IsAny<DTOType>())).Returns(entity);
            repo.Setup(m => m.CreateAsync(It.IsAny<EntityType>())).ReturnsAsync(42);
            var adapterRepo = CreateAdapterRepo(repo.Object, adapter.Object);


            var result = await adapterRepo.CreateAsync(dto);

            adapter.Verify(m => m.ConvertToEntity(It.Is<DTOType>(d => d.Equals(dto))), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Calls_Update_On_Repo_With_Converted_Entity_Returns_Id()
        {
            var dto = CreateSampleDTO();
            var entity = CreateSampleEntity();
            var adapter = CreateAdapterMock();
            var repo = CreateRepoMock();

            adapter.Setup(m => m.ConvertToEntity(It.IsAny<DTOType>())).Returns(entity);
            repo.Setup(m => m.UpdateAsync(It.IsAny<EntityType>())).ReturnsAsync(true);
            var adapterRepo = CreateAdapterRepo(repo.Object, adapter.Object);

            var result = await adapterRepo.UpdateAsync(dto);

            repo.Verify(m => m.UpdateAsync(It.Is<EntityType>(e => e.Equals(entity))), Times.Once());
            Assert.True(result);
        }
    }
}
