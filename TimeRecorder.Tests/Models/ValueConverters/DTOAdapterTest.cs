using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.ValueConverters;
using Xunit;

namespace TimeRecorder.Tests.Models.ValueConverters
{
    public class DTOAdapterTest
    {

        public ProjectDTO CreateTestProject()
        {
            return new ProjectDTO
            {
                Id = 1,
                Color = Colors.Green,
                Name = "Some name",
                TemporaryId = 1,
            };
        }
        public RecordingDTO CreateTestDTO()
        {
            var time = TimeSpan.FromHours(2);
            return new RecordingDTO
            {
                Title = "Some title",
                Start = DateTime.Parse("18-03-2019") - time,
                End = DateTime.Parse("18-03-2019") + time,
                Id = 1,
                ProjectId = 1,
                TemporaryId = 1,
                Project = CreateTestProject(),
            };
        }
        [Fact]
        public void Test_ToSummaryVM_Given_DTO_With_Null_Project_Returns_VM_With_Placeholder_values()
        {
            var dto = CreateTestDTO();
            dto.Project = null;
            dto.ProjectId = null;

            var result = DTOAdapter.ToSummaryVM(dto);

            Assert.Equal("No Project Chosen", result.ProjectName);
            Assert.Equal(Colors.LightGray, result.ProjectColor.Color);
        }

        [Fact]
        public void Test_ToSummaryVM_Given_DTO_With_Valid_Project_Returns_Matching_VM()
        {
            var dto = CreateTestDTO();

            var result = DTOAdapter.ToSummaryVM(dto);

            Assert.Equal("Some name", result.ProjectName);
            Assert.Equal(Colors.Green, result.ProjectColor.Color);
        }

        [Fact]
        public void Test_ToSummaryVM_Given_DTO_With_Valid_Times_Returns_VM_With_duration()
        {
            var dto = CreateTestDTO();
            var expected = "4:00:00";

            var result = DTOAdapter.ToSummaryVM(dto);

            Assert.Equal(expected, result.Duration);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_ToSummaryVM_Given_DTO_With_No_Title_Returns_VM_With_Placeholder(string title)
        {
            var dto = CreateTestDTO();
            dto.Title = title;

            var result = DTOAdapter.ToSummaryVM(dto);

            Assert.Equal("No description", result.Title);
        }

    }
}
