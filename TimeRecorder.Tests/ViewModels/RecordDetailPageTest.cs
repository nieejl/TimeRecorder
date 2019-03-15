using Moq;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services.LocalStorage;
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
    }
}
