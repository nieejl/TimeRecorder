using System;
using System.Collections.Generic;
using System.Windows.Media;
using TimeRecorder.Models.DTOs;
using TimeRecorder.ViewModels;
using Xunit;

namespace TimeRecorder.Tests.ViewModels
{
    public class RecordDetailPageTest
    {
        private ProjectDTO CreateTestProject()
        {
            return new ProjectDTO
            {
                Name = "Some Project",
                Color = Colors.Black
            };
        }
        private RecordingDTO CreateTestRecording()
        {
            return new RecordingDTO
            {
                Title = "Some Title",
                StartDate = new DateTime(2019, 1, 11),
                EndDate = new DateTime(2019, 1, 12),
                StartTime = new TimeSpan(2, 32, 5),
                EndTime = new TimeSpan(5, 32, 10),
                Project = CreateTestProject(),
                Tags = new List<string>{ "firstTag", "secondTag" }
            };
        }
        [Fact]
        public void Test_UpdateFromDTO_Updates_Title()
        {
            var vm = new RecordingDetailPageVM();
            var dto = CreateTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal(dto.Title, vm.Title);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_StartDate()
        {
            var vm = new RecordingDetailPageVM();
            var dto = CreateTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("11-01-2019", vm.StartDate);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_EndDate()
        {
            var vm = new RecordingDetailPageVM();
            var dto = CreateTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("12-01-2019", vm.EndDate);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_StartTime()
        {
            var vm = new RecordingDetailPageVM();
            var dto = CreateTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("02:32:05", vm.StartTime);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_EndTime()
        {
            var vm = new RecordingDetailPageVM();
            var dto = CreateTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("05:32:10", vm.EndTime);
        }

        [Fact]
        public void Test_UpdateFromDTO_Updates_ElapsedTime()
        {
            var vm = new RecordingDetailPageVM();
            var dto = CreateTestRecording();

            vm.UpdateFromDTO(dto);

            Assert.Equal("1.03:00:05", vm.ElapsedTime);
        }
    }
}
