using RestSharp;
using System.Threading.Tasks;

namespace skarpa.core.Data.ApiConnect
{
    public interface IApiRequest<TRest>
    {
        Task<IRestResponse<TRest>> MakeRequestAsync(object postdata, string url, Method method);
    }
}
