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
                Title = "Test title one",
            };
        }


        public static List<RecordingDTO> CreateThreeRecordingDTOs()
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

        public static Recording CreateRecording()
        {
            return new Recording
            {
                Id = 1,
                Start = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(13),
                End = DateTime.Parse("16-03-2019") - TimeSpan.FromHours(12),
                ProjectId = 1,
                TemporaryId = 2,
                Title = "Test title one",
                LastUpdated = DateTime.Parse("16-03-2019")
            };
        }

        public static List<Recording> CreateThreeRecordings()
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
                Name = "Project Test one",
                Argb = 123321,
            };
        }

        public static List<ProjectDTO> CreateThreeProjectDTOs()
        {
            var p1 = new ProjectDTO
            {
                Id = 1,
                TemporaryId = 1,
                Name = "Project Test one",
                Argb = 123321,
            };

            var p2 = new ProjectDTO
            {
                Id = 2,
                TemporaryId = 2,
                Name = "Project Test two",
                Argb = 456654,
            };

            var p3 = new ProjectDTO
            {
                Id = 3,
                TemporaryId = 3,
                Name = "Project Test three",
                Argb = 789987,
            };
            return new List<ProjectDTO> { p1, p2, p3 };
        }

        public static Project CreateProject()
        {
            return new Project
            {
                Id = 1,
                TemporaryId = 1,
                Name = "Project Test one",
                Argb = 123321,
            };
        }

        public static List<Project> CreateThreeProjects()
        {
            var p1 = new Project
            {
                Id = 1,
                TemporaryId = 1,
                Name = "Project Test one",
                Argb = 123321,
            };

            var p2 = new Project
            {
                Id = 2,
                TemporaryId = 2,
                Name = "Project Test two",
                Argb = 456654,
            };

            var p3 = new Project
            {
                Id = 3,
                TemporaryId = 3,
                Name = "Project Test three",
                Argb = 789987,
            };
            return new List<Project> { p1, p2, p3 };
        }




    }
}
