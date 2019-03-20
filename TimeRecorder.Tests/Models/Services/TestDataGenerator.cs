using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Tests.Models.Services
{
    public class TestDataGenerator
    {
        public static RecordingDTO CreateRecording(int id)
        {
            return new RecordingDTO
            {

                Id = id,
                Title = "Sample value project",
                ProjectId = 1,
                Start = DateTime.Parse("01-02-2019"),
                End = DateTime.Parse("02-02-2019"),
                TemporaryId = 1,
            };
        }

        public static RecordingDTO CreateDifferentRecording(int id)
        {
            return new RecordingDTO
            {
                Id = id,
                Title = "Another Sample project",
                ProjectId = 2,
                Start = DateTime.Parse("07-04-1999"),
                End = DateTime.Parse("08-04-1999"),
                TemporaryId = 2,
            };
        }

        public static ProjectDTO CreateProject(int id)
        {
            return new ProjectDTO
            {
                Id = id,
                TemporaryId = id,
                Argb = 1234321,
                Name = "Some name"
            };
        }

        public static ProjectDTO CreateDifferentProject(int id)
        {
            return new ProjectDTO
            {
                Id = id,
                TemporaryId = id,
                Argb = 4567654,
                Name = "Some different name"
            };
        }
    }
}
