using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.Models.Services.LocalStorage;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;

namespace TimeRecorder.Models.Services.ServerStorage
{
    public class RecordingOnlineRepository : 
        AbstractOnlineCrudRepo<RecordingDTO>, 
        IRecordingRepository
    {
        string readAmount;

        protected override string entityName => "recording";

        public RecordingOnlineRepository(IHttpClient client) : base(client)
        {
        }
        protected override void SetCustomRoutes(string basePath)
        {
            readAmount = basePath + "/read/";
        }

        public async Task<IEnumerable<RecordingDTO>> ReadAmount(int amount, int startIndex = 0)
        {
            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                var response = await client.GetAsync(readAmount + $"{amount}/{startIndex}");
                if (response.IsSuccessAndNotNull())
                {
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<IEnumerable<RecordingDTO>>(result);
                    //return await response.Content.ReadAsAsync<List<RecordingDTO>>().ConfigureAwait(false);
                }
                    //return await response.Content.ReadAsAsync<List<RecordingDTO>>(client.Formatters).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Debug.WriteLine("caught exception: " + e.Message);
            }
            return new List<RecordingDTO>();
        }

    }
}
