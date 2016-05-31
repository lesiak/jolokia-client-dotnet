using System.Collections.Generic;
using Jolokia.Client.Jmx;

namespace Jolokia.Client.Request
{
    public abstract class AbtractJ4pMBeanRequest : J4pRequest
    {
        // name of MBean to execute a request on
        private readonly ObjectName objectName;

        protected AbtractJ4pMBeanRequest(J4pType pRequestType, ObjectName pMBeanName, J4pTargetConfig pTargetConfig) 
            : base(pRequestType, pTargetConfig)
        {
          
            objectName = pMBeanName;
        }

        internal override List<string> getRequestParts()
        {
            List<string> ret = new List<string>();
            ret.Add(objectName.getCanonicalName());
            return ret;

        }
    }
}