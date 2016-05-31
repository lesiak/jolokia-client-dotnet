using System;
using System.Collections.Generic;

namespace Jolokia.Client.Request
{
    public class ValidatingResponseExtractor : IJ4pResponseExtractor
    {
        public static IJ4pResponseExtractor DEFAULT = new ValidatingResponseExtractor();

        public RESP Extract<RESP, REQ>(REQ request, Dictionary<string, object> pJsonResp) where RESP : J4pResponse<REQ> where REQ : J4pRequest
        {
            //throw new System.NotImplementedException();

            int status = pJsonResp.ContainsKey("status") ?
               Convert.ToInt32((long)pJsonResp["status"]) :
               0;
            Console.WriteLine("Status: {0}", status);
            return null;
        }
    }
}