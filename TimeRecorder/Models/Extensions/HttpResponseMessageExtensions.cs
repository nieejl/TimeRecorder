using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static bool IsSuccessAndNotNull(this HttpResponseMessage response)
        {
            return response != null && response.IsSuccessStatusCode;
        }
    }
}