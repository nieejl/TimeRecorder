using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.ServerStorage;

namespace TimeRecorder.Tests.Models.Services.ServerStorage
{
    public class ProjectOnlineCrudRepoTest :
        AbstractOnlineCrudRepoTest<ProjectDTO, ProjectOnlineRepository>
    {
        public override ProjectOnlineRepository CreateRepo(IHttpClient client)
        {
            return new ProjectOnlineRepository(client);
        }

        public override ProjectDTO CreateSampleValue(int id = 1)
        {
            return TestDataGenerator.CreateProject(id);
        }
    }
}
