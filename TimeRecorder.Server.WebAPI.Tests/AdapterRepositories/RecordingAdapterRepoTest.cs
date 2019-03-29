using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using TimeRecorder.Server.WebAPI.AdapterRepositories;
using TimeRecorder.Server.WebAPI.Adapters;
using TimeRecorder.Shared;
using Xunit;

namespace TimeRecorder.Server.WebAPI.Tests.AdapterRepositories
{
    public class RecordingAdapterRepoTest : 
        AbstractAdapterRepoTest<
            RecordingAdapterRepo, 
            IRecordingRepository, 
            RecordingDTO, Recording>
    {
        public override Mock<IAdapter<RecordingDTO, Recording>> CreateAdapterMock()
        {
            return new Mock<IAdapter<RecordingDTO, Recording>>();
        }

        public override RecordingAdapterRepo CreateAdapterRepo(
            IRecordingRepository repo, IAdapter<RecordingDTO, Recording> adapter)
        {
            return new RecordingAdapterRepo(repo, adapter);
        }

        public override Mock<IRecordingRepository> CreateRepoMock()
        {
            return new Mock<IRecordingRepository>();
        }

        public override RecordingDTO CreateSampleDTO()
        {
            return TestDataGenerator.CreateRecordingDTO();
        }

        public override Recording CreateSampleEntity()
        {
            return TestDataGenerator.CreateRecording();
        }

        public override List<RecordingDTO> CreateThreeSampleDTOs()
        {
            return TestDataGenerator.CreateThreeRecordingDTOs();
        }

        public override List<Recording> CreateThreeSampleEntities()
        {
            return TestDataGenerator.CreateThreeRecordings();
        }

        [Fact]
        public async Task ReadAmount_Given_2_1_Returns_2_Elements_From_Index_1()
        {
            var entities = CreateThreeSampleEntities();
            var dtos = TestDataGenerator.CreateThreeRecordingDTOs().Take(2).Reverse(); 
            var repo = new Mock<IRecordingRepository>();
            repo.Setup(r => r.Read()).ReturnsAsync(entities.AsQueryable());
            var adapter = new RecordingAdapter();
            var adapterRepo = new RecordingAdapterRepo(repo.Object, adapter);

            var result = await adapterRepo.ReadAmount(2, 1);
            Assert.Equal(dtos, result);
        }
    }
}
