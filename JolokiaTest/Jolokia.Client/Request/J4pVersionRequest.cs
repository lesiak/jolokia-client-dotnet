﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pVersionRequest : J4pRequest<J4pVersionResponse>
    {
        /// <summary>
        /// Plain Constructor
        /// </summary>
        public J4pVersionRequest() : this(null)
        {            
        }
        
        /// <summary>
        /// Constructor with using a proxy configuration
        /// </summary>
        /// <param name="pConfig">proxy configuration for a JSR-160 proxy</param>
        public J4pVersionRequest(J4pTargetConfig pConfig) : base(J4pType.VERSION, pConfig)
        {            
        }

        public override List<string> GetRequestParts()
        {
            return new List<string>();
        }

        internal override J4pVersionResponse CreateResponse(JObject pResponse)
        {
            return new J4pVersionResponse(this, pResponse);
        }
    }
}