using System;
using System.Collections.Generic;
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
        public void SerializeArgumentToRequestPartArrayTest()
        {
            string[] stringArray = {"aElem1", "aElem2" };
            Assert.AreEqual("aElem1,aElem2", RequestSerializer.SerializeArgumentToRequestPart(stringArray));
        }

        [TestMethod]
        public void SerializeArgumentToRequestPartListTest()
        {
            var stringList = new List<string>
            {
                "lElem1",
                "lElem2"
            };
            Assert.AreEqual("lElem1,lElem2", RequestSerializer.SerializeArgumentToRequestPart(stringList));
        }
    }
}
