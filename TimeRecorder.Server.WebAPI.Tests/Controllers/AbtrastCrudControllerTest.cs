using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Server.WebAPI.Controllers;
using TimeRecorder.Shared;
using Xunit;

namespace TimeRecorder.Server.WebAPI.Tests.Controllers
{
    public abstract class AbstractCrudControllerTest<ControllerType, RepoType, DTOType, EntityType>
        where ControllerType : AbstractCrudController<DTOType, EntityType>
        where RepoType : class, IAdapterRepo<DTOType, EntityType>
        where DTOType : DTO
        where EntityType : Entity
    {
        public abstract DTOType CreateSampleValue();
        public abstract ControllerType CreateController(RepoType adapterRepo);
        public abstract Mock<RepoType> CreateMock();
        public abstract DTOType CreateSampleNullValue();
        
        [Fact]
        public async Task Test_FindAsync_Given_Null_Returns_NotFoundObjectResult()
        {
            var mock = CreateMock();
            var dto = CreateSampleNullValue();
            mock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(dto);
            var controller = CreateController(mock.Object);

            var result = await controller.FindAsync(42);

            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async Task Test_PostAsync_Given_0_Returns_NotFoundObjectResult()
        {
            var mock = CreateMock();
            var dto = CreateSampleValue();
            mock.Setup(m => m.CreateAsync(It.IsAny<DTOType>())).ReturnsAsync(0);
            var controller = CreateController(mock.Object);

            var result = await controller.PostAsync(dto);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Test_PutAsync_Given_False_Returns_NotFoundObjectResult()
        {
            var mock = CreateMock();
            mock.Setup(m => m.UpdateAsync(It.IsAny<DTOType>())).ReturnsAsync(false);
            var controller = CreateController(mock.Object);
            var dto = CreateSampleValue();

            var result = await controller.PutAsync(dto);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Test_DeleteAsync_Given_False_Returns_NotFoundObjectResult()
        {
            var mock = CreateMock();
            mock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);
            var dto = CreateSampleValue();
            var controller = CreateController(mock.Object);

            var result = await controller.DeleteAsync(42);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Test_FindAsync_Given_DTO_Returns_DTO()
        {
            var mock = CreateMock();
            var dto = CreateSampleValue();
            mock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(dto);
            var controller = CreateController(mock.Object);

            var result = await controller.FindAsync(42);

            Assert.Equal(dto, result.Value);
        }

        [Fact]
        public async Task Test_PostAsync_Given_True_Returns_True()
        {
            var mock = CreateMock();
            var dto = CreateSampleValue();
            mock.Setup(m => m.CreateAsync(It.IsAny<DTOType>())).ReturnsAsync(1);
            var controller = CreateController(mock.Object);

            var result = await controller.PostAsync(dto);

            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task Test_PutAsync_Given_True_Returns_Ok()
        {
            var mock = CreateMock();
            var dto = CreateSampleValue();
            mock.Setup(m => m.UpdateAsync(It.IsAny<DTOType>())).ReturnsAsync(true);
            var controller = CreateController(mock.Object);

            var result = await controller.PutAsync(dto);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Test_DeleteAsync_Given_True_Returns_Ok()
        {
            var mock = CreateMock();
            var dto = CreateSampleValue();
            mock.Setup(m => m.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
            var controller = CreateController(mock.Object);

            var result = await controller.DeleteAsync(42);

            Assert.IsType<OkObjectResult>(result.Result);
        }

    }
}
