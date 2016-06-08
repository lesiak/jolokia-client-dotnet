﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jolokia.Client;
using Jolokia.Client.Request;


namespace JolokiaTest
{
    class Program
    {

        static void ReadRequest(J4pClient j4pClient)
        {
            J4pReadRequest req = new J4pReadRequest("java.lang:type=Memory", "HeapMemoryUsage");
            //J4pReadRequest req = new J4pReadRequest("java.lang:type=MemoryPool,name=Code Cache", "PeakUsage");
            try
            {
                Task<J4pReadResponse> resp = j4pClient.Execute(req);
                resp.Wait();
                /* foreach (var entry in resp.Result.JsonResponse)
                 {
                     Console.WriteLine(entry);
                 }*/
                Console.WriteLine(resp.Result.JsonResponse);
                var vals = resp.Result.GetValue<IDictionary<string, object>>();
                Console.WriteLine(vals);
                Console.WriteLine(vals["used"]);
                Console.WriteLine(vals["used"].GetType());

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0]);
            }
        }


        static void Main(string[] args)
        {
            J4pClient j4pClient = new J4pClient("http://localhost:8080/jolokia");
            ReadRequest(j4pClient);
            try
            {
                J4pVersionRequest versionReq = new J4pVersionRequest();
                Task<J4pVersionResponse> verResponseTask = j4pClient.Execute(versionReq);
                verResponseTask.Wait();
                J4pVersionResponse verResponse = verResponseTask.Result;
                //Console.WriteLine(verResponse.getProduct());
                Console.WriteLine("Agent Version: " + verResponse.AgentVersion);
                Console.WriteLine("Protocol Version: " + verResponse.AgentVersion);
                Console.WriteLine("Realms: " + string.Join(", ", verResponse.Realms));

            }

            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0]);
            }


            Console.ReadLine();
        }
    }


}
