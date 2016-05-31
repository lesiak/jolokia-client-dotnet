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

        // ==================================================================================================
        // Methods used for building up HTTP Requests and setting up the reponse
        // These methods are assembly visible only since are used only internally

        // Get the parts to build up a GET url (without the requestType as the first part)
        internal abstract List<string> getRequestParts();


        /**
     * Get the requestType of the request
     *
     * @return request's requestType
     */
        public J4pType getRequestType()
        {
            return requestType;
        }

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