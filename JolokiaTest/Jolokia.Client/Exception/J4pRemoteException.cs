using System.Collections.Generic;
using Jolokia.Client.Request;

namespace Jolokia.Client.Exception
{
    public class J4pRemoteException : J4pException
    {
        public J4pRemoteException(IJ4pRequest pJ4pRequest, Dictionary<string, object> pJsonRespObject) : base(pJsonRespObject["error"] != null ?
                          (string)pJsonRespObject["error"] :
                          "Invalid response received: " + pJsonRespObject.ToString())
        {
           
            long? statusL = (long?)pJsonRespObject["status"];
            //status = statusL != null ? statusL.intValue() : 500;
            //request = pJ4pRequest;
            //errorType = (String)pJsonRespObject.get("error_type");
            //remoteStacktrace = (String)pJsonRespObject.get("stacktrace");
            //errorValue = (JSONObject)pJsonRespObject.get("error_value");
        }
    }
}