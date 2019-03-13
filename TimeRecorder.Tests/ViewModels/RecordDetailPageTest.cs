using Moq;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services.Repositories;
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
        private Mock<IRecordingRepository> CreateTestRepo()
        {
            return new Mock<IRecordingRepository>();
        }

        private IRecordingRepository CreateEmptyRepo()
        {
            return CreateTestRepo().Object;
        }
        private List<TagDTO> createTestTags()
        {
            return new List<TagDTO>
            {
                new TagDTO { Id = 1, TagValue = "firstTag"},
                new TagDTO { Id = 1, TagValue = "secondTag"}
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
                Tags = createTestTags()
            };
        }

        private RecordingDetailPageVM createTestVM()
        {
            var mockFactory = CreateValidityFactory(true, true);
            var mockProjectRepo = new Mock<IProjectRepository>();
            mockFactory.SetupAllProperties();
            return new RecordingDetailPageVM(CreateEmptyRepo(), mockProjectRepo.Object, mockFactory.Object);
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
        public void Test_UpdateFromDTO_Updates_Tags()
        {
            var vm = createTestVM();
            var dto = createTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("firstTag, secondTag", vm.Tags);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_StartDate()
        {
            var vm = createTestVM();
            var mockField = new Mock<IDateFieldVM>();
            var dto = createTestRecording();
            vm.StartDateFieldVM = mockField.Object;

            vm.UpdateFromDTO(dto);

            mockField.VerifySet(field => field.ParsedDate = dto.Start);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_EndDate()
        {
            var vm = createTestVM();
            var mockField = new Mock<IDateFieldVM>();
            var dto = createTestRecording();
            vm.EndDateFieldVM = mockField.Object;

            vm.UpdateFromDTO(dto);

            mockField.VerifySet(field => field.ParsedDate = dto.End.Value);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_StartTime()
        {
            var vm = createTestVM();
            var mockField = new Mock<ITimeFieldVM>();
            var dto = createTestRecording();
            vm.StartTimeFieldVM = mockField.Object;

            vm.UpdateFromDTO(dto);

            mockField.VerifySet(field => field.TextField = "2:32");
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_EndTime()
        {
            var vm = createTestVM();
            var mockField = new Mock<ITimeFieldVM>();
            var dto = createTestRecording();
            vm.EndTimeFieldVM = mockField.Object;

            vm.UpdateFromDTO(dto);

            mockField.VerifySet(field => field.TextField = "5:32");
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_ElapsedTime()
        {
            var vm = createTestVM();
            var mockField = new Mock<ITimeFieldVM>();
            var dto = createTestRecording();
            vm.ElapsedTimeFieldVM = mockField.Object;

            vm.UpdateFromDTO(dto);

            mockField.VerifySet(field => field.TextField = "27:00");
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
        public void Test_TrySaveToDTO_Returns_True_If_Time_And_Date_Is_Valid()
        {
            var dto = createTestRecording();
            var mockFactory = CreateValidityFactory(true, true);
            var mockProjectRepo = new Mock<IProjectRepository>();

            var vm = new RecordingDetailPageVM(CreateEmptyRepo(), mockProjectRepo.Object, mockFactory.Object);

            var result = vm.TrySaveToDTO();

            Assert.True(result);
        }

        [Fact]
        public void Test_TrySaveToDTO_Returns_False_If_Time_And_Date_Is_Invalid()
        {
            var dto = createTestRecording();
            var mockFactory = CreateValidityFactory(false, false);
            var mockProjectRepo = new Mock<IProjectRepository>();

            var vm = new RecordingDetailPageVM(CreateEmptyRepo(), mockProjectRepo.Object, mockFactory.Object);

            var result = vm.TrySaveToDTO();

            Assert.False(result);
        }
    }
}
