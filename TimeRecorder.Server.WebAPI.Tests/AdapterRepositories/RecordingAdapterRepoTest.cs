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
using Xunit;

namespace TimeRecorder.Server.WebAPI.Tests.AdapterRepositories
{
    public class RecordingAdapterRepoTest
    {

        [Fact]
        public async Task ReadAmount_Given_2_1_Returns_2_Elements_From_Index_1()
        {
            var entities = TestDataGenerator.CreateTestRecordings().ToList();
            var dtos = TestDataGenerator.CreateTestRecordingDTOs().Take(2).Reverse(); 
            var repo = new Mock<IRecordingRepository>();
            repo.Setup(r => r.Read()).ReturnsAsync(entities.AsQueryable());
            var adapter = new RecordingAdapter();
            var adapterRepo = new RecordingAdapterRepo(repo.Object, adapter);

            var result = await adapterRepo.ReadAmount(2, 1);
            Assert.Equal(dtos, result);
        }
    }
}
