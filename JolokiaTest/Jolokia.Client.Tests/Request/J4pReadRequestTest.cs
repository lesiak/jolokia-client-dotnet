using Jolokia.Client.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jolokia.Client.Tests.Request
{
    [TestClass]
    public class J4pReadRequestTest
    {
        [TestMethod]
        public void TestGetRequestParts()
        {
            J4pReadRequest req = new J4pReadRequest("java.lang:type=Memory", "HeapMemoryUsage");
            Assert.AreEqual(2, req.getRequestParts().Count);
        }
    }
}
