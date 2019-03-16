using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services.LocalStorage;
using System.Net.Http;

namespace TimeRecorder.Models.Services.ServerStorage
{
    public class RecordingOnlineRepository : 
        AbstractOnlineCrudRepo<RecordingDTO>, 
        IRecordingRepository
    {
        string readAmount;
        public RecordingOnlineRepository(IHttpClient client) : base(client)
        {
        }
        protected override void SetCustomRoutes(string basePath)
        {
            readAmount = basePath + "/read/";
        }

        public async Task<IEnumerable<RecordingDTO>> ReadAmount(int amount, int startIndex = 0)
        {
            var response = await client.GetAsync(read + $"{amount}/{startIndex}");
            if (response.IsSuccessAndNotNull())
                return await response.Content.ReadAsAsync<List<RecordingDTO>>(client.Formatters);
            return new List<RecordingDTO>();
        }

        Task<IQueryable<RecordingDTO>> IRecordingRepository.ReadAmount(int amount, int startIndex)
        {
            throw new NotImplementedException();
        }
    }
}
