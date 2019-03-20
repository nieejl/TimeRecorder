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
        public override RecordingDTO CreateSampleValue(int id = 1)
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


        // TODO: Remove if not needed
        //public override IQueryable<RecordingDTO> GetQueryable()
        //{
        //    var dtos = new List<RecordingDTO>() { CreateSampleValue(), CreateDifferentSampleValue(2) };
        //    return dtos.AsQueryable();
        //}
    }
}
