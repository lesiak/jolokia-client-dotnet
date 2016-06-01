using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public interface IJ4pResponse
    {
    }

    public abstract class J4pResponse<TReq> : IJ4pResponse where TReq : IJ4pRequest
    {
        // JSON representation of the returned response
        private JObject jsonResponse;

        // request which lead to this response
        private TReq request;

        protected J4pResponse(TReq pRequest, JObject pJsonResponse)
        {
            request = pRequest;
            jsonResponse = pJsonResponse;
            //Long timestamp = (Long)jsonResponse.get("timestamp");
            //requestDate = timestamp != null ? new Date(timestamp * 1000) : new Date();
        }

        /// <summary>
        /// Get the value of this response
        /// </summary>        
        /// <returns>json representation of answer</returns>
        public IDictionary<string, object> GetValue()
        {
            return (IDictionary<string, object>) (jsonResponse["value"] as JObject).ToObject(typeof (IDictionary<string, object>));// as IDictionary<string, object>;
        }

        public JObject JsonResponse
        {
            get { return jsonResponse; }
        }
    }
}