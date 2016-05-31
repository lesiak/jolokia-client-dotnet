using System;
using System.Collections.Generic;
using Jolokia.Client.Exception;

namespace Jolokia.Client.Request
{
    public class ValidatingResponseExtractor : IJ4pResponseExtractor
    {
        public static IJ4pResponseExtractor DEFAULT = new ValidatingResponseExtractor();

        private readonly HashSet<int> allowedCodes;

        public ValidatingResponseExtractor(params int[] pCodesAllowed)
        {
            allowedCodes = new HashSet<int>();
            // 200 is always contained
            allowedCodes.Add(200);
            foreach (int code in pCodesAllowed)
            {
                allowedCodes.Add(code);
            }
        }

        public RESP Extract<RESP, REQ>(REQ pRequest, Dictionary<string, object> pJsonResp) where RESP : J4pResponse<REQ> where REQ : J4pRequest
        {
            //throw new System.NotImplementedException();

            int status = pJsonResp.ContainsKey("status") ?
               Convert.ToInt32((long)pJsonResp["status"]) :
               0;
            Console.WriteLine("Status: {0}", status);
            if (!allowedCodes.Contains(status))
            {
                throw new J4pRemoteException(pRequest, pJsonResp);
            }
            
            return status == 200 ? pRequest.CreateResponse<RESP, REQ>(pJsonResp) : null;
        }
    }
}