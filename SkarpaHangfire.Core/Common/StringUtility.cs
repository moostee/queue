using RestSharp;
using System.Threading.Tasks;

namespace SkarpaHangfire.Core.Common
{
    public static class StringUtility
    {
        public async static Task<IRestResponse<T>> MakeApiRequest<T>(RestSharp.Method method, string url, string parameter = null, string clientSecret = null)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = method == Method.POST ? new RestRequest(Method.POST) : new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            if (clientSecret != null)
                request.AddHeader("X-ClientSecret", clientSecret);
            if (method == Method.POST)
                request.AddParameter("application/json", parameter, ParameterType.RequestBody);
            return await client.ExecuteAsync<T>(request);
        }
    }
}
