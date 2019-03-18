using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.RepositoryInterfaces;
using TimeRecorder.Models.Services.Strategies;
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels;
using TimeRecorder.ViewModels.Interfaces;
using Xunit;

namespace TimeRecorder.Tests.ViewModels
{
    public class RecordDetailPageTest
    {
        private ProjectDTO createTestProject()
        {
            return new ProjectDTO
            {
                Name = "Some Project",
                Color = Colors.Black
            };
        }
        
        private RecordingDTO createTestRecording()
        {
            var startTime = new TimeSpan(2, 32, 5);
            var endTime = new TimeSpan(5, 32, 10);
            return new RecordingDTO
            {
                Title = "Some Title",
                Start = new DateTime(2019, 1, 11) + startTime,
                End = new DateTime(2019, 1, 12) + endTime,
                Project = createTestProject(),
            };
        }

        private Mock<IRecordingStrategy> createTestFindRecordingStrategy(RecordingDTO dto)
        {
            var recordingRepo = new Mock<IRecordingRepository>();
            recordingRepo.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(dto);

            var recordingStrategy = new Mock<IRecordingStrategy>();
            recordingStrategy.Setup(m => m.CreateRepository(It.IsAny<StorageStrategy>())).Returns(recordingRepo.Object);

            return recordingStrategy;
        }

        private Mock<IProjectStrategy> createTestFindProjectStrategy(ProjectDTO dto)
        {
            var projectRepo = new Mock<IProjectRepository>();
            projectRepo.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(dto);

            var projectStrategy = new Mock<IProjectStrategy>();
            projectStrategy.Setup(m => m.CreateRepository(It.IsAny<StorageStrategy>())).Returns(projectRepo.Object);
            return projectStrategy;
        }

        [Fact]
        public async Task Test_UpdateFromDTO_Fills_In_StartTimeText_If_End_Not_Null()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.UpdateFromDTO(2);
            await Task.Delay(500);

            Assert.Equal(recording.Start.TimeOfDay.ToHHMMSS(), vm.StartText);
        }

        [Fact]
        public async Task Test_UpdateFromDTO_Fills_In_EndTimeText_If_End_Not_Null()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.UpdateFromDTO(2);
            await Task.Delay(500);

            Assert.Equal(recording.End.Value.TimeOfDay.ToHHMMSS(), vm.EndText);
        }

        [Fact]
        public async Task Test_UpdateFromDTO_Sets_ElapsedTimeText_To_Difference_If_End_Not_Null()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);
            var expected = recording.End.Value - recording.Start;

            vm.UpdateFromDTO(2);
            await Task.Delay(500);

            Assert.Equal(expected.ToHHMMSS(), vm.ElapsedText);
        }


        [Fact]
        public async Task Test_UpdateFromDTO_Sets_EndTimeText_To_Null_If_End_Is_Null()
        {
            var recording = createTestRecording();
            recording.End = null;
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.UpdateFromDTO(2);
            await Task.Delay(500);

            Assert.Null(vm.EndText);
        }

        [Fact]
        public void Test_GetValidityColor_Given_True_Returns_Transparent()
        {
            var recording = createTestRecording();
            recording.End = null;
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            var result = vm.GetValidityColor(true).Color;

            Assert.Equal(Colors.Transparent, result);
        }

        [Fact]
        public void Test_GetValidityColor_Given_False_Returns_Red()
        {
            var recording = createTestRecording();
            recording.End = null;
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            var result = vm.GetValidityColor(false).Color;

            Assert.Equal(Colors.Red, result);
        }
        
        [Fact]
        public void Test_ChooseColor_Given_White_Sets_ChosenColor_To_White()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);
            var expectedColor = Colors.White;

            vm.ChooseColor(new SolidColorBrush(expectedColor));

            Assert.Equal(expectedColor, vm.ChosenColor.Color);
        }

        [Fact]
        public async Task Test_IsStartValid_Given_Valid_Times_Returns_True()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.StartText = "23:59";

            Assert.True(vm.IsStartValid);
        }

        [Fact]
        public async Task Test_IsEndValid_Given_Valid_Times_Returns_True()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.EndText = "12:34";

            Assert.True(vm.IsEndValid);
        }

        [Fact]
        public void Test_IsElapsedValid_Given_Valid_Times_Returns_True()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.ElapsedText = "10:00";

            Assert.True(vm.IsElapsedValid);
        }

        [Fact]
        public async Task Test_IsStartValid_Given_Invalid_Times_Returns_False()
        {
            var recording = createTestRecording();
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.StartText = "284";

            Assert.False(vm.IsStartValid);
        }

        [Fact]
        public async Task Test_IsEndValid_Given_Invalid_Times_Returns_False()
        {
            var recording = createTestRecording();
            recording.End = null;
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            vm.UpdateFromDTO(recording);
            await Task.Delay(1000);
            vm.EndText = "1005";

            Assert.False(vm.IsEndValid);
        }

        [Fact]
        public void Test_IsElapsedValid_Given_Invalid_Times_Returns_False()
        {
            var recording = createTestRecording();
            recording.End = null;
            var recordingStrategy = createTestFindRecordingStrategy(recording);
            var projectStrategy = createTestFindProjectStrategy(null);
            var vm = new RecordingDetailPageVM(recordingStrategy.Object, projectStrategy.Object);

            //vm.UpdateFromDTO(recording);
            //await Task.Delay(1000);
            vm.ElapsedText = "263";

            Assert.False(vm.IsElapsedValid);
        }
    }
}
