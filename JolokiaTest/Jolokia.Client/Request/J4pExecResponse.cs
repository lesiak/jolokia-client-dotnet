using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pExecResponse : J4pResponse<J4pExecRequest>
    {
        public J4pExecResponse(J4pExecRequest pRequest, JObject pJsonResponse) : base(pRequest, pJsonResponse)
        {            
        }
    }
}