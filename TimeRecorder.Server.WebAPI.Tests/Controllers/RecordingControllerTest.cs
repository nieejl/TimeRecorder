using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Server.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TimeRecorder.Shared;
using Xunit;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using System.Linq;

namespace TimeRecorder.Server.WebAPI.Tests.Controllers
{
    public class RecordingControllerTest : 
        AbstractCrudControllerTest<RecordingController, IRecordingAdapterRepo, RecordingDTO, Recording>
    {
        public override RecordingController CreateController(IRecordingAdapterRepo adapterRepo)
        {
            return new RecordingController(adapterRepo);
        }

        public override Mock<IRecordingAdapterRepo> CreateMock()
        {
            return new Mock<IRecordingAdapterRepo>();
        }

        public override RecordingDTO CreateSampleValue() => TestDataGenerator.CreateRecordingDTO();

        public override RecordingDTO CreateSampleNullValue() => null;

        private int unwrapAsNotFoundStatusCode<T>(ActionResult<T> actionResult)
        {
            return (actionResult as NotFoundResult).StatusCode;
        }
        private object unwrapAsOk<T>(ActionResult<T> actionResult)
        {
            return (actionResult.Result as OkObjectResult).Value;
        }

        [Fact]
        public async Task ReadAmount_Given_Empty_From_Repo_Returns_404()
        {
            var fromRepo = new List<RecordingDTO>();
            var mock = new Mock<IRecordingAdapterRepo>();
            mock.Setup(m => m.ReadAmount(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(fromRepo);
            var controller = new RecordingController(mock.Object);

            var actionResult = await controller.ReadAmount(5, 10);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task ReadAmount_Given_List_Returns_List()
        {
            var fromRepo = TestDataGenerator.CreateTestRecordingDTOs();
            var mock = new Mock<IRecordingAdapterRepo>();
            mock.Setup(m => m.ReadAmount(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(fromRepo);
            var controller = new RecordingController(mock.Object);

            var actionResult = await controller.ReadAmount(5, 10);
            var result = unwrapAsOk(actionResult);

            Assert.Equal(fromRepo, result);
        }
    }
}
