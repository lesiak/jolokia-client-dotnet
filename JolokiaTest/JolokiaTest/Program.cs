using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jolokia.Client;
using Jolokia.Client.Request;


namespace JolokiaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            J4pClient j4pClient = new J4pClient("http://localhost:8080/jolokia");

            J4pReadRequest req = new J4pReadRequest("java.lang:type=Memory", "HeapMemoryUsage");
            //J4pReadRequest req = new J4pReadRequest("java.lang:type=MemoryPool,name=Code Cache", "PeakUsage");
            try
            {
                Task<J4pReadResponse> resp = j4pClient.Execute(req);                               
                resp.Wait();                
                foreach (var entry in resp.Result.JsonResponse)
                {
                    Console.WriteLine(entry);
                }
                object vals = resp.Result.GetValue();
                Console.WriteLine(vals);

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0]);
            }
            Console.ReadLine();
        }
    }
}
