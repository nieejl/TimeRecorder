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

namespace TimeRecorder.Server.WebAPI.Tests.Controllers
{
    public class RecordingControllerTest
    {
        private object unwrap<T>(ActionResult<T> actionResult)
        {
            return (actionResult.Result as OkObjectResult).Value;
        }

        [Fact]
        public async Task ReadAmount_Given_Empty_From_Repo_Returns_Empty()
        {
            var mock = new Mock<IRecordingAdapterRepo>();
            var expected = new List<RecordingDTO>();
            mock.Setup(m => m.ReadAmount(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expected);
            var controller = new RecordingController(mock.Object);

            var actionResult = await controller.ReadAmount(5, 10);
            var result = unwrap(actionResult);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ReadAmount_Given_List_Returns_List()
        {
            var mock = new Mock<IRecordingAdapterRepo>();
            var expected = TestDataGenerator.CreateTestRecordingDTOs();
            mock.Setup(m => m.ReadAmount(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expected);
            var controller = new RecordingController(mock.Object);

            var actionResult = await controller.ReadAmount(5, 10);
            var result = unwrap(actionResult);

            Assert.Equal(expected, result);
        }

        private object TestDataGeneratorCreateTestRecordingDTOs()
        {
            throw new NotImplementedException();
        }
    }
}
