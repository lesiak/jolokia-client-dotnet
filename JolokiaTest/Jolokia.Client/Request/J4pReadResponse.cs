using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pReadResponse : J4pResponse<J4pReadRequest>
    {        
        public J4pReadResponse(J4pReadRequest j4pReadRequest, JObject pResponse) : base(j4pReadRequest, pResponse)
        {            
        }        
    }
}