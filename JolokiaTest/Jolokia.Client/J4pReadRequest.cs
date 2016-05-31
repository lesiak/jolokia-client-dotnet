using System;
using System.Collections.Generic;
using System.Linq;

namespace Jolokia.Client
{
    public class J4pReadRequest : AbtractJ4pMBeanRequest
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

        internal override List<string> getRequestParts()
        {
            /** {@inheritDoc} */
            
                if (hasSingleAttribute())
                {
                    List<string> ret = base.getRequestParts();
                    ret.Add(getAttribute());
                    addPath(ret, path);
                    return ret;
                }
                else if (hasAllAttributes() && path == null)
                {
                    return base.getRequestParts();
                }

                // A GET request cant be used for multiple attribute fetching or for fetching
                // all attributes with a path
                return null;
            
        }


        /**
    * If this request is for a single attribute, this attribute is returned
    * by this getter.
    * @return single attribute
    * @throws IllegalArgumentException if no or more than one attribute are used when this request was
    *         constructed.
    */
        public String getAttribute()
        {
            if (!hasSingleAttribute())
            {
                throw new ArgumentException("More than one attribute given for this request");
            }
            return attributes[0];
        }

        /**
     * Whether this request represents a request for a single attribute
     *
     * @return true if the client request is for a single attribute
     */
        public bool hasSingleAttribute()
        {
            return attributes.Count == 1;
        }

        /**
         * Whether all attributes should be fetched
         *
         * @return true if all attributes should be fetched
         */
        public bool hasAllAttributes()
        {
            return attributes.Count == 0;
        }

    }
}