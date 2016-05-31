using System;
using System.Collections.Generic;

namespace Jolokia.Client.Request
{
    public abstract class J4pRequest
    {


        // request requestType
        private readonly J4pType requestType;

        /**
     * 
     * @param pRequestType 
     * @param pTargetConfig 
     */
        /// <summary>
        /// Constructor for subclasses
        /// </summary>
        /// <param name="pRequestType">requestType of this request</param>
        /// <param name="pTargetConfig">a target configuration if used in proxy mode or <code>null</code>
        ///                      if this is a direct request</param>
        protected J4pRequest(J4pType pRequestType, J4pTargetConfig pTargetConfig)
        {
            requestType = pRequestType;
            //    targetConfig = pTargetConfig;
        }



        /// <summary>
        /// Get the requestType of the request
        /// </summary>
        /// <returns>request's requestType</returns>
        public J4pType getRequestType()
        {
            return requestType;
        }

        // ==================================================================================================
        // Methods used for building up HTTP Requests and setting up the reponse
        // These methods are assembly visible only since are used only internally

        // Get the parts to build up a GET url (without the requestType as the first part)
        internal abstract List<string> getRequestParts();




        /**
          * Create a response from a given JSON response
          *
          * @param pResponse http response as obtained from the Http-Request
          * @return the create response
          */
        internal abstract RESP CreateResponse<RESP, REQ>(Dictionary<string, object> pResponse)
              where RESP : J4pResponse<REQ>
              where REQ : J4pRequest;

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