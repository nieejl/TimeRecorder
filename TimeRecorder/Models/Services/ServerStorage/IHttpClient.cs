using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.Services.ServerStorage
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsJsonAsync(string location, object item);
        Task<HttpResponseMessage> PutAsJsonAsync(string location, object item);
        Task<HttpResponseMessage> GetAsync(string location);
        Task<HttpResponseMessage> DeleteAsync(string location);
        MediaTypeFormatter[] Formatters { get; }
    }
}
