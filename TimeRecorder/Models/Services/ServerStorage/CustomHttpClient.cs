using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.Services.ServerStorage
{
    public class CustomHttpClient : HttpClient, IHttpClient
    {

        public CustomHttpClient(string baseUri, string mediaType)
        {
            BaseAddress = new Uri(baseUri);
            DefaultRequestHeaders.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        private StringContent serializeContent(object item)
        {
            return new StringContent(JsonConvert.SerializeObject(item), 
                Encoding.UTF8, "application/json");
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync(string location, object item)
        {
            return await PostAsync(location, serializeContent(item));
        }

        public async Task<HttpResponseMessage> PutAsJsonAsync(string location, object item)
        {
            return await PutAsync(location, serializeContent(item));
        }
    }
}
