using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jolokia.Client;
using Jolokia.Client.Request;
using Newtonsoft.Json.Linq;


namespace JolokiaTest
{
    class Program
    {

        static async void ReadRequest(J4pClient j4pClient)
        {
            var req = new J4pReadRequest("java.lang:type=Memory", "HeapMemoryUsage");
            //J4pReadRequest req = new J4pReadRequest("java.lang:type=MemoryPool,name=Code Cache", "PeakUsage");

            var resp = await j4pClient.Execute(req);

            /* foreach (var entry in resp.Result.JsonResponse)
             {
                 Console.WriteLine(entry);
             }*/
            Console.WriteLine(resp.JsonResponse);
            var vals = resp.GetValue<IDictionary<string, object>>();
            Console.WriteLine(vals);
            Console.WriteLine(vals["used"]);
            Console.WriteLine(vals["used"].GetType());


        }


        static async void VersionRequest(J4pClient j4pClient)
        {
            var versionReq = new J4pVersionRequest();                       
            var verResponse = await j4pClient.Execute(versionReq);
            //Console.WriteLine(verResponse.getProduct());
            Console.WriteLine("Agent Version: " + verResponse.AgentVersion);
            Console.WriteLine("Protocol Version: " + verResponse.AgentVersion);
            Console.WriteLine("Realms: " + string.Join(", ", verResponse.Realms));
        }

        static async void ListRequest(J4pClient j4pClient)
        {
            var listReq = new J4pListRequest("java.lang/type=OperatingSystem/attr");
            var listResponse = await j4pClient.Execute(listReq);
            var respValue = listResponse.GetValueAsDictionary();
            Console.WriteLine("listResponseValue" + respValue);
            
            var freePhysicalMemorySize = (JObject) respValue["FreePhysicalMemorySize"];
            
            
            Console.WriteLine("MemorySize attruibute: " + freePhysicalMemorySize);
            Console.WriteLine("MemorySize desc: " + freePhysicalMemorySize["desc"]);
            Console.WriteLine("Request date:" + listResponse.RequestDate);
        }

        static async Task ExecRequest(J4pClient j4pClient)
        {           
            var listReq = new J4pExecRequest("java.util.logging:type=Logging", "getLoggerLevel", "org.apache.jasper");
            var listResponse = await j4pClient.Execute(listReq);
            var respValue = listResponse.GetValue<object>();
            Console.WriteLine("execResponseValue" + respValue);           
        }


        static void Main(string[] args)
        {
            var j4pClient = new J4pClient("http://localhost:8080/jolokia");          
            try
            {
                //ReadRequest(j4pClient);
                //VersionRequest(j4pClient);
                //ListRequest(j4pClient);

                ExecRequest(j4pClient).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                int aaa = 0;
            }
            Console.ReadLine();
        }
    }


}
