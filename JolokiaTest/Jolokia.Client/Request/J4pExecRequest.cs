using System;
using System.Collections.Generic;
using System.Linq;
using Jolokia.Client.Jmx;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pExecRequest : AbtractJ4pMBeanRequest<J4pExecResponse>
    {

        // Operation to execute
        private string operation;

        // Operation arguments
        private List<object> arguments;

        /// <summary>
        /// New client request for executing a JMX operation
        /// </summary>
        /// <param name="pMBeanName">name of the MBean to execute the request on</param>
        /// <param name="pOperation">operation to execute</param>
        /// <param name="pArgs">any arguments to pass (which must match the JMX operation's declared signature)</param>
        public J4pExecRequest(ObjectName pMBeanName, string pOperation, params object[] pArgs) 
            : this(null, pMBeanName, pOperation, pArgs)
        {            
        }

        /// <summary>
        /// New client request for executing a JMX operation
        /// </summary>
        /// <param name="pTargetConfig">proxy target configuration or <code>null</code> if no proxy should be used</param>
        /// <param name="pMBeanName">name of the MBean to execute the request on</param>
        /// <param name="pOperation">operation to execute</param>
        /// <param name="pArgs">any arguments to pass (which must match the JMX operation's declared signature)</param>
        public J4pExecRequest(J4pTargetConfig pTargetConfig, ObjectName pMBeanName, string pOperation, params object[] pArgs) 
            : base(J4pType.EXEC, pMBeanName, pTargetConfig)
        {            
            operation = pOperation;
            if (pArgs == null)
            {
                // That's the case when a single, null argument is given (which is the only
                // case that pArgs can be null)
                arguments = new List<object> {null};
            }
            else
            {
                arguments = pArgs.ToList();
            }
        }

        /// <summary>
        /// New client request for executing a JMX operation
        /// </summary>
        /// <param name="pMBeanName">name of the MBean to execute the request on</param>
        /// <param name="pOperation">operation to execute</param>
        /// <param name="pArgs">any arguments to pass (which must match the JMX operation's declared signature)</param>
        public J4pExecRequest(string pMBeanName, string pOperation, params object[] pArgs) : this(null, pMBeanName, pOperation, pArgs)
            //throws MalformedObjectNameException
        {        
        }

        /// <summary>
        /// New client request for executing a JMX operation
        /// </summary>
        /// <param name="pTargetConfig">proxy target configuration or <code>null</code> if no proxy should be used</param>
        /// <param name="pMBeanName">name of the MBean to execute the request on</param>
        /// <param name="pOperation">operation to execute</param>
        /// <param name="pArgs">any arguments to pass (which must match the JMX operation's declared signature)</param>
        public J4pExecRequest(J4pTargetConfig pTargetConfig, string pMBeanName, string pOperation, params object[] pArgs) : this(pTargetConfig, new ObjectName(pMBeanName), pOperation, pArgs)
            //throws MalformedObjectNameException
        {       
        }

        internal override J4pExecResponse CreateResponse(JObject pResponse)
        {
            return new J4pExecResponse(this, pResponse);
        }

        public override List<string> GetRequestParts()
        {
            List<string> ret = base.GetRequestParts();
            ret.Add(operation);
            if (arguments.Count > 0)
            {
                for (int i = 0; i < arguments.Count; i++)
                {
                    ret.Add(SerializeArgumentToRequestPart(arguments[i]));
                }
            }
            return ret;
        }

    }

}