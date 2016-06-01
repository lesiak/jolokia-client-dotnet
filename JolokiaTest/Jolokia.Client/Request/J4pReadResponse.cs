using System.Collections.Generic;

namespace Jolokia.Client.Request
{
    public class J4pReadResponse : J4pResponse<J4pReadRequest>
    {
        private J4pReadRequest j4pReadRequest;
        private Dictionary<string, object> pResponse;

        public J4pReadResponse(J4pReadRequest j4pReadRequest, Dictionary<string, object> pResponse)
        {
            this.j4pReadRequest = j4pReadRequest;
            this.pResponse = pResponse;
        }

        public Dictionary<string, object> Response
        {
            get { return pResponse; }            
        }
    }
}