using System;
using System.Collections.Generic;
using System.Text;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.Tests
{
    public class TestDataGenerator
    {
        public static RecordingDTO CreateRecordingDTO()
        {
            return new RecordingDTO
            {
                Id = 1,
                Start = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(13),
                End = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(12),
                ProjectId = 1,
                TemporaryId = 2,
                Title = "Test title",
            };
        }


        public static IEnumerable<RecordingDTO> CreateTestRecordingDTOs()
        {
            var r1 = new RecordingDTO
            {
                Id = 1,
                Start = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(13),
                End = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(12),
                ProjectId = 1,
                TemporaryId = 2,
                Title = "Test title one",
            };
            var r2 = new RecordingDTO
            {
                Id = 2,
                Start = DateTime.Parse("17-03-2019") - TimeSpan.FromHours(8),
                End = DateTime.Parse("17-03-2019") - TimeSpan.FromHours(4),
                ProjectId = 2,
                TemporaryId = 3,
                Title = "Test title two",
            };

            var r3 = new RecordingDTO
            {
                Id = 3,
                Start = DateTime.Parse("18-03-2019") - TimeSpan.FromHours(5),
                End = DateTime.Parse("18-03-2019") - TimeSpan.FromHours(3),
                ProjectId = 3,
                TemporaryId = 4,
                Title = "Test title three",
            };
            return new List<RecordingDTO> { r1, r2, r3 };
        }

        public static IEnumerable<Recording> CreateTestRecordings()
        {
            var r1 = new Recording
            {
                Id = 1,
                Start = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(13),
                End = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(12),
                ProjectId = 1,
                TemporaryId = 2,
                Title = "Test title one",
                LastUpdated = DateTime.Parse("16-03-2019")
            };
            var r2 = new Recording
            {
                Id = 2,
                Start = DateTime.Parse("17-03-2019") - TimeSpan.FromHours(8),
                End = DateTime.Parse("17-03-2019") - TimeSpan.FromHours(4),
                ProjectId = 2,
                TemporaryId = 3,
                Title = "Test title two",
                LastUpdated = DateTime.Parse("17-03-2019")
            };

            var r3 = new Recording
            {
                Id = 3,
                Start = DateTime.Parse("18-03-2019") - TimeSpan.FromHours(5),
                End = DateTime.Parse("18-03-2019") - TimeSpan.FromHours(3),
                ProjectId = 3,
                TemporaryId = 4,
                Title = "Test title three",
                LastUpdated = DateTime.Parse("18-03-2019")
            };
            return new List<Recording> { r1, r2, r3 };
        }

        public static ProjectDTO CreateProjectDTO()
        {
            return new ProjectDTO
            {
                Id = 1,
                TemporaryId = 1,
                Name = "Projest Test name",
                Argb = 123321,
            };
        }
    }
}
