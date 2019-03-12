using Moq;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using TimeRecorder.Models.DTOs;
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
                Tags = new List<string>{ "firstTag", "secondTag" }
            };
        }

        private RecordingDetailPageVM createTestVM()
        {
            var mockFactory = new Mock<IParserFieldVMFactory>();
            return new RecordingDetailPageVM(mockFactory.Object);
        }


        [Fact]
        public void Test_UpdateFromDTO_Updates_Title()
        {
            var vm = createTestVM();
            var dto = createTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal(dto.Title, vm.Title);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_StartDate()
        {
            var vm = createTestVM();
            var dto = createTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("11-01-2019", vm.StartDate);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_EndDate()
        {
            var vm = createTestVM();
            var dto = createTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("12-01-2019", vm.EndDate);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_StartTime()
        {
            var vm = createTestVM();
            var dto = createTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("02:32:05", vm.StartTime);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_EndTime()
        {
            var vm = createTestVM();
            var dto = createTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("05:32:10", vm.EndTime);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_ElapsedTime()
        {
            var vm = createTestVM();
            var dto = createTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("1.03:00:05", vm.ElapsedTime);
        }

        private Mock<IDateFieldVM> CreateValidityDateField(bool isValid)
        {
            var mockDateField = new Mock<IDateFieldVM>();
            mockDateField.Setup(m => m.IsValid).Returns(isValid);
            return mockDateField;
        }
        private Mock<ITimeFieldVM> CreateValidityTimeField(bool isValid)
        {
            var mockTimeField = new Mock<ITimeFieldVM>();
            mockTimeField.Setup(m => m.IsValid).Returns(isValid);
            return mockTimeField;
        }

        private Mock<IParserFieldVMFactory> CreateValidityFactory(bool timeValid, bool dateValid)
        {
            var mockFactory = new Mock<IParserFieldVMFactory>();
            var mockTimeField = CreateValidityTimeField(timeValid);
            var mockDateField = CreateValidityDateField(dateValid);
            mockFactory.Setup(m => m.Generate24HourTimeField()).Returns(mockTimeField.Object);
            mockFactory.Setup(m => m.GenerateDateField()).Returns(mockDateField.Object);
            mockFactory.Setup(m => m.GenerateUnlimitedTimeField()).Returns(mockTimeField.Object);
            return mockFactory;
        }

        [Fact]
        public void Test_TrySaveToDTO_Saves_To_DTO_If_Time_And_Date_Is_Valid()
        {
            var dto = createTestRecording();
            var mockFactory = CreateValidityFactory(true, true);

            var vm = new RecordingDetailPageVM(mockFactory.Object);
            vm.TrySaveToDTO();

            mockFactory.Verify();
        }
    }
}
