using Jolokia.Client.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jolokia.Client.Tests.Request
{
   

    [TestClass]
    public class J4pListRequestTest
    {
        [TestMethod]
        public void TestGetRequestParts()
        {
            var splitPath = J4pListRequest.SplitPath("java.lang/type=OperatingSystem/attr");
            Assert.AreEqual(3, splitPath.Count);
        }
    }
}