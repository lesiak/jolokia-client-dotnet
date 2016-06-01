using System.Collections.Generic;
using Jolokia.Client.Jmx;

namespace Jolokia.Client.Request
{
    public abstract class AbtractJ4pMBeanRequest<TResp> : J4pRequest<TResp>
    {
        // name of MBean to execute a request on
        private readonly ObjectName objectName;

        protected AbtractJ4pMBeanRequest(J4pType pRequestType, ObjectName pMBeanName, J4pTargetConfig pTargetConfig) 
            : base(pRequestType, pTargetConfig)
        {
          
            objectName = pMBeanName;
        }

        public override List<string> getRequestParts()
        {
            List<string> ret = new List<string>
            {
                objectName.getCanonicalName()
            };            
            return ret;

        }
    }
}