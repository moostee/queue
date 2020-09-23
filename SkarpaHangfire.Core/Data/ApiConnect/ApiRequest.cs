using Newtonsoft.Json;
using RestSharp;
using skarpa.core.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace skarpa.core.Data.ApiConnect
{
    public class ApiRequest<TRest> : IApiRequest<TRest>
    {
        protected RestClient client;
        protected RestRequest request;
        protected string baseurl { get; set; }
        protected string baseapp { get; set; }
        protected string contentType { get; set; }
        protected string acceptType { get; set; }

        protected virtual void InitializeRequest()
        {
            client = new RestClient();
            client.Timeout = -1;
            request = new RestRequest();
            request.AddHeader("Content-Type", contentType);
        }
        protected void AddHeader(string key, string value)
        {
            if (request != null)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    request.AddHeader(key, value);
                }
            }
        }

        protected string SerializeData(object postdata)
        {
            var resp = "";
            switch (contentType)
            {
                case "application/json":
                    resp = JsonConvert.SerializeObject(postdata);
                    break;
                case "application/x-www-form-urlencoded":
                    var properties = from p in postdata.GetType().GetProperties()
                                     where p.GetValue(postdata, null) != null
                                     select p.Name + "=" + p.GetValue(postdata, null).ToString();

                    resp = String.Join("&", properties.ToArray());
                    break;
            }
            return resp;
        }

        public async Task<IRestResponse<TRest>> MakeRequestAsync(object postdata, string url, Method method)
        {
            client.BaseUrl = string.IsNullOrEmpty(baseapp) ? new Uri(baseurl + url) : new Uri(baseurl + baseapp + url);
            try
            {
                switch (method)
                {
                    case Method.GET:
                        request.Method = Method.GET;
                        request.AddParameter(acceptType, postdata, ParameterType.QueryStringWithoutEncode);
                        break;
                    case Method.POST:
                        request.Method = Method.POST;
                        request.AddParameter(acceptType, postdata, ParameterType.RequestBody);
                        break;
                    case Method.PUT:
                        request.Method = Method.POST;
                        break;
                    case Method.DELETE:
                        request.Method = Method.POST;
                        break;
                    default:
                        break;
                }
                return await client.ExecuteAsync<TRest>(request);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw ex;
            }
        }
    }
}
