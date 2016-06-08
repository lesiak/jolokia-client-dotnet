using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pListResponse : J4pResponse<J4pListRequest>
    {
        public J4pListResponse(J4pListRequest pRequest, JObject pJsonResponse) : base(pRequest, pJsonResponse)
        {            
        }
    }
}