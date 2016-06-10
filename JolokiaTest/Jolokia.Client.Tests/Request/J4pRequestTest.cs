using System;
using Jolokia.Client.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jolokia.Client.Tests.Request
{
    [TestClass]
    public class J4pRequestTest
    {
        [TestMethod]
        public void NullEscapeTest()
        {
            Assert.AreEqual("[null]", RequestSerializer.NullEscape(null));
        }


        [TestMethod]
        public void SerializeArgumentToRequestPartTest()
        {
            string[] stringArray = {"aaa", "bbbb"};
            Assert.AreEqual("aaa,bbbb", RequestSerializer.SerializeArgumentToRequestPart(stringArray));
        }
    }
}
