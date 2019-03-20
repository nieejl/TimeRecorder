using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.ServerStorage;

namespace TimeRecorder.Tests.Models.Services.ServerStorage
{
    public class RecordingOnlineCrudRepoTest :
        AbstractOnlineCrudRepoTest<RecordingDTO, RecordingOnlineRepository>
    {
        public override RecordingOnlineRepository CreateRepo(IHttpClient client)
        {
            return new RecordingOnlineRepository(client);
        }

        public override RecordingDTO CreateSampleValue(int id = 1)
        {
            return TestDataGenerator.CreateRecording(id);
        }
    }
}
