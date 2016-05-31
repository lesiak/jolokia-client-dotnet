using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jolokia.Client;


namespace JolokiaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            J4pClient j4pClient = new J4pClient("http://localhost:8080/jolokia");

            J4pReadRequest req = new J4pReadRequest("java.lang:type=Memory",
                "HeapMemoryUsage");
            try
            {
                Task<J4pReadResponse> resp = j4pClient.execute(req);
                if (resp != null)
                {
                    resp.Wait();
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0]);
            }
            Console.ReadLine();
        }
    }
}
