using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public abstract class J4pRequest<TResp> : IJ4pRequest
    {        
        /// <summary>
        /// Constructor for subclasses
        /// </summary>
        /// <param name="pRequestType">requestType of this request</param>
        /// <param name="pTargetConfig">a target configuration if used in proxy mode or <code>null</code>
        ///                      if this is a direct request</param>
        protected J4pRequest(J4pType pRequestType, J4pTargetConfig pTargetConfig)
        {
            RequestType = pRequestType;
            //    targetConfig = pTargetConfig;
        }

        /// <summary>
        /// Get the requestType of the request
        /// </summary>
        /// <returns>request's requestType</returns>
        public J4pType RequestType { get; }

        // ==================================================================================================
        // Methods used for building up HTTP Requests and setting up the reponse
        // These methods are assembly visible only since are used only internally

        // Get the parts to build up a GET url (without the requestType as the first part)
        public abstract List<string> GetRequestParts();

        /// <summary>
        /// Create a response from a given JSON response
        /// </summary>
        /// <param name="pResponse">http response as obtained from the Http-Request</param>
        /// <returns>the created response</returns>
        internal abstract TResp CreateResponse(JObject pResponse);
             

        // Helper class
        protected void addPath(List<string> pParts, string pPath)
        {
            if (pPath == null)
            {
                return;
            }
            throw new NotImplementedException();
        }

       
    }
}