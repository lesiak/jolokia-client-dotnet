using System;
using System.Collections.Generic;
using System.Text;
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

        
        protected string SerializeArgumentToRequestPart(object pArg)
        {
            return RequestSerializer.SerializeArgumentToRequestPart(pArg);
        }

    }

    public static class RequestSerializer
    {

        /// <summary>
        /// Serialize an object to a string which can be uses as URL part in a GET request
        /// when object should be transmitted <em>to</em> the agent. The serialization is
        /// rather limited: If it is an array, the array's member's string representation are used
        /// in a comma separated list (without escaping so far, so the strings must not contain any
        /// commas themselves). If it is not an array, the string representation ist used (<code>Object.toString()</code>)
        /// Any <code>null</code> value is transformed in the special marker <code>[null]</code> which on the
        /// agent side is converted back into a <code>null</code>.
        /// <p>
        /// You should consider POST requests when you need a more sophisticated JSON serialization.
        /// </p>
        /// </summary>
        /// <param name="pArg">the argument to serialize for an GET request</param>
        /// <returns>the string representation</returns>
        public static string SerializeArgumentToRequestPart(object pArg)
        {
            if (pArg != null)
            {
                if (pArg.GetType().IsArray)
                {
                    return GetArrayForArgument((object[]) pArg);
                }
                /*else if (List.class.isAssignableFrom(pArg.getClass())) {
                    List list = (List)pArg;
                    Object[] args = new Object[list.size()];
                    int i = 0;
                    for (Object e : list) {
                        args[i++] = e;
                    }
                    return getArrayForArgument(args);
                    }*/
            }
            return NullEscape(pArg);
        }

        private static string GetArrayForArgument(object[] pArg)
        {
            StringBuilder inner = new StringBuilder();
            for (int i = 0; i < pArg.Length; i++)
            {
                inner.Append(NullEscape(pArg[i]));
                if (i < pArg.Length - 1)
                {
                    inner.Append(",");
                }
            }
            return inner.ToString();
        }

        // null escape used for GET requests
        public static string NullEscape(object pArg)
        {
            if (pArg == null)
            {
                return "[null]";
            }
            else if (pArg is string && ((string)pArg).Length == 0)
            {
                return "\"\"";
            } /*else if (pArg is JSONAware) {
                return ((JSONAware)pArg).toJSONString();
            } */
            else {
                return pArg.ToString();
            }
        }
    }
}