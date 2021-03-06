﻿using System;
using System.Collections.Generic;
using Jolokia.Client.Exception;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class ValidatingResponseExtractor : IJ4pResponseExtractor
    {
        public static IJ4pResponseExtractor DEFAULT = new ValidatingResponseExtractor();

        private readonly HashSet<int> allowedCodes;

        public ValidatingResponseExtractor(params int[] pCodesAllowed)
        {
            allowedCodes = new HashSet<int>
            {
                200 //200 is always contained
            };                       
            foreach (int code in pCodesAllowed)
            {
                allowedCodes.Add(code);
            }
        }

        public RESP Extract<RESP>(J4pRequest<RESP> pRequest, JObject pJsonResp) where RESP : class, IJ4pResponse
        {
            //throw new System.NotImplementedException();

            int status = ((IDictionary<string, JToken>)pJsonResp).ContainsKey("status") ?
               Convert.ToInt32((long)pJsonResp["status"]) :
               0;
            Console.WriteLine("Status: {0}", status);
            if (!allowedCodes.Contains(status))
            {
                throw new J4pRemoteException(pRequest, pJsonResp);
            }

            return status == 200 ? pRequest.CreateResponse(pJsonResp) : null;
           // return status == 200 ? pRequest.CreateResponse(pJsonResp) : default(RESP);
        }
    }
}