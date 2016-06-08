using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jolokia.Client;
using Jolokia.Client.Request;


namespace JolokiaTest
{
    class Program
    {

        static async void ReadRequest(J4pClient j4pClient)
        {
            J4pReadRequest req = new J4pReadRequest("java.lang:type=Memory", "HeapMemoryUsage");
            //J4pReadRequest req = new J4pReadRequest("java.lang:type=MemoryPool,name=Code Cache", "PeakUsage");

            J4pReadResponse resp = await j4pClient.Execute(req);

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
            J4pVersionRequest versionReq = new J4pVersionRequest();                       
            J4pVersionResponse verResponse = await j4pClient.Execute(versionReq);
            //Console.WriteLine(verResponse.getProduct());
            Console.WriteLine("Agent Version: " + verResponse.AgentVersion);
            Console.WriteLine("Protocol Version: " + verResponse.AgentVersion);
            Console.WriteLine("Realms: " + string.Join(", ", verResponse.Realms));
        }

        static async void ListRequest(J4pClient j4pClient)
        {

            J4pListRequest listReq = new J4pListRequest("java.lang/type=OperatingSystem/attr");
            J4pListResponse listResponse = await j4pClient.Execute(listReq);
            Console.WriteLine(listResponse.GetValue<object>());           
        }


        static void Main(string[] args)
        {
            var j4pClient = new J4pClient("http://localhost:8080/jolokia");          
            try
            {
                //ReadRequest(j4pClient);
                //VersionRequest(j4pClient);
                ListRequest(j4pClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }


}
