using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Tests.Models.Services.LocalStorage
{
    public class ProjectLocalRepositoryTest : AbstractCrudRepoTest<ProjectDTO, ProjectLocalRepository>
    {
        public override void CopyToFrom(ProjectDTO to, ProjectDTO from)
        {
            to.Name = from.Name;
            to.TemporaryId = from.TemporaryId;
            to.Id = from.Id;
            to.Argb = from.Argb;
        }

        public override ProjectDTO CreateDifferentSampleValue(int id = 1)
        {
            return new ProjectDTO
            {
                Id = id,
                TemporaryId = id,
                Argb = 4567654,
                Name = "Some different name"
            };
        }

        public override ProjectLocalRepository CreateRepo(ITimeRecorderContext context)
        {
            return new ProjectLocalRepository(context);
        }

        public override ProjectDTO CreateSampleValue(int id = 1)
        {
            return new ProjectDTO
            {
                Id = id,
                TemporaryId = id,
                Argb = 1234321,
                Name = "Some name"
            };
        }
    }
}
