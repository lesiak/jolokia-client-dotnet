using System;
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
            long? unixTimestampSeconds = (long?)pJsonResponse["timestamp"];
            
            RequestDate = unixTimestampSeconds != null 
                ? DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds.Value).DateTime 
                : DateTime.Now;
        }

        /// <summary>
        /// Get the request/response type
        /// </summary>
        /// <returns>type</returns>
        public J4pType RequestType => request.RequestType;

        /// <summary>
        /// Date when the request was processed
        /// </summary>
        /// <returns>request date</returns>
        public DateTime RequestDate
        {
            //no need to clone - DateTime is immutable value type
            get;
        }

        /// <summary>
        /// Get the value of this response
        /// </summary>        
        /// <returns>json representation of answer</returns>
        public V GetValue<V>()
        {
            return GetValueAsJObject().ToObject<V>();
        }

        protected JObject GetValueAsJObject()
        {
            return JsonResponse["value"] as JObject;
        }

        public IDictionary<string, JToken> GetValueAsDictionary()
        {
            return GetValueAsJObject();
        }

    }
}