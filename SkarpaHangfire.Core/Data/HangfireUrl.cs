using Newtonsoft.Json;
using SkarpaHangfire.Core.Common;
using System.Threading.Tasks;

namespace SkarpaHangfire.Core.Data
{
    public static class HangfireUrl
    {
        public async static Task<string> Call(string url)
        {
            var response = await StringUtility.MakeApiRequest<object>(RestSharp.Method.GET, url);
            return JsonConvert.SerializeObject(response.Data);
        }
    }
}
