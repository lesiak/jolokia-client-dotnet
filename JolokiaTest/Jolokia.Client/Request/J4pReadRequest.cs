using System;
using System.Collections.Generic;
using System.Linq;
using Jolokia.Client.Jmx;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pReadRequest : AbtractJ4pMBeanRequest<J4pReadResponse>
    {

        // Name of attribute to request
        private readonly List<string> attributes;

        // Path for extracting return value
        private String path;

        /// <summary>
        /// Create a READ request to request one or more attributes
        /// from the remote j4p agent
        /// </summary>
        /// <param name="pObjectName"> object name as sting which gets converted to a {@link javax.management.ObjectName}}</param>
        /// <param name="">zero, one or more attributes to request.</param>
        /// <exception cref=""></exception>
        public J4pReadRequest(string pObjectName, params string[] pAttribute) : this(null, pObjectName, pAttribute)
        {
        }

        /// <summary>
        /// Create a READ request to request one or more attributes
        /// from the remote j4p agent
        /// </summary>
        /// <param name="pTargetConfig"> proxy target configuration or <code>null</code> if no proxy should be used</param>
        /// <param name="pObjectName">object name as sting which gets converted to a {@link javax.management.ObjectName}}</param>
        /// <param name="pAttribute">zero, one or more attributes to request.</param>
        public J4pReadRequest(J4pTargetConfig pTargetConfig, string pObjectName, params string[] pAttribute)
            : this(pTargetConfig, new ObjectName(pObjectName), pAttribute)
        //throws MalformedObjectNameException
        {
        }

        public J4pReadRequest(J4pTargetConfig pTargetConfig, ObjectName pObjectName, params string[] pAttribute)
            : base(J4pType.READ, pObjectName, pTargetConfig)
        {
            attributes = pAttribute.ToList();
        }

        /// <summary>
        /// If this request is for a single attribute, this attribute is returned by this getter.
        /// </summary>
        /// <returns>single attribute</returns>
        public string getAttribute()
        {
            if (!HasSingleAttribute())
            {
                throw new ArgumentException("More than one attribute given for this request");
            }
            return attributes[0];
        }

        public override List<string> GetRequestParts()
        {
            if (HasSingleAttribute())
            {
                List<string> ret = base.GetRequestParts();
                ret.Add(getAttribute());
                addPath(ret, path);
                return ret;
            }
            else if (HasAllAttributes() && path == null)
            {
                return base.GetRequestParts();
            }

            // A GET request cant be used for multiple attribute fetching or for fetching
            // all attributes with a path
            return null;
        }
   
        internal override J4pReadResponse CreateResponse(JObject pResponse)
        {
            return new J4pReadResponse(this, pResponse);
        }

        /// <summary>
        /// Whether this request represents a request for a single attribute
        /// </summary>
        /// <returns>true if the client request is for a single attribute</returns>
        public bool HasSingleAttribute()
        {
            return attributes.Count == 1;
        }       

        /// <summary>
        /// Whether all attributes should be fetched
        /// </summary>
        /// <returns>true if all attributes should be fetched</returns>
        public bool HasAllAttributes()
        {
            return attributes.Count == 0;
        }

    }
}