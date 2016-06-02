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
        public JObject JsonResponse { get; }

        // request which lead to this response
        private TReq request;

        protected J4pResponse(TReq pRequest, JObject pJsonResponse)
        {
            request = pRequest;
            JsonResponse = pJsonResponse;
            //Long timestamp = (Long)jsonResponse.get("timestamp");
            //requestDate = timestamp != null ? new Date(timestamp * 1000) : new Date();
        }

        /// <summary>
        /// Get the value of this response
        /// </summary>        
        /// <returns>json representation of answer</returns>
        public V GetValue<V>()
        {
            return GetValueAsJObject().ToObject<V>();
        }

        private JObject GetValueAsJObject()
        {
            return JsonResponse["value"] as JObject;
        }

        private IDictionary<string, object> GetValueAsDictionary()
        {
            return GetValueAsJObject().ToObject<IDictionary<string, object>>();
        }

    }
}