using System.Collections.Generic;

namespace Jolokia.Client.Request
{
    public class J4pReadResponse : J4pResponse<J4pReadRequest>
    {        
        public J4pReadResponse(J4pReadRequest j4pReadRequest, Dictionary<string, object> pResponse) : base(j4pReadRequest, pResponse)
        {            
        }        
    }
}