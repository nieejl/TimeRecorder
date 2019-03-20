using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Tests.Models.Services.LocalStorage
{
    public class RecordingLocalRepositoryTest :
        AbstractCrudRepoTest<RecordingDTO, RecordingLocalRepository>
    {
        public override RecordingDTO CreateDifferentSampleValue(int id = 1)
        {
            return TestDataGenerator.CreateDifferentRecording(id);
        }
        public override RecordingDTO CreateSampleValue(int id = 1)
        {
            return TestDataGenerator.CreateRecording(id);
        }

        public override RecordingLocalRepository CreateRepo(ITimeRecorderContext context)
        {
            return new RecordingLocalRepository(context);
        }

        public override void CopyToFrom(RecordingDTO to, RecordingDTO from)
        {
            to.End = from.End;
            to.Start = from.Start;
            to.Id = from.Id;
            to.ProjectId = from.ProjectId;
            to.Title = from.Title;
            to.Project = from.Project;
            to.Tags = from.Tags;
            to.TemporaryId = from.TemporaryId;
        }
    }
}
