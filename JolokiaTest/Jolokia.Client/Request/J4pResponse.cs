using System.Collections.Generic;

namespace Jolokia.Client.Request
{
    public interface IJ4pResponse
    {
    }

    public abstract class J4pResponse<TReq> : IJ4pResponse where TReq : IJ4pRequest
    {
        // JSON representation of the returned response
        private Dictionary<string, object> jsonResponse;

        // request which lead to this response
        private TReq request;

        protected J4pResponse(TReq pRequest, Dictionary<string, object> pJsonResponse)
        {
            request = pRequest;
            jsonResponse = pJsonResponse;
            //Long timestamp = (Long)jsonResponse.get("timestamp");
            //requestDate = timestamp != null ? new Date(timestamp * 1000) : new Date();
        }

        /// <summary>
        /// Get the value of this response
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <returns>json representation of answer</returns>
        public object GetValue()
        {
            return jsonResponse["value"];
        }

        public Dictionary<string, object> JsonResponse
        {
            get { return jsonResponse; }
        }
    }
}