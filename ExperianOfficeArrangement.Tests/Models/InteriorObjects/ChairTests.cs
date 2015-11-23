using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    [TestClass]
    public class ChairTests
    {
        [TestClass]
        public class CtorTests
        {
            [TestMethod]
            public void ShouldSetPropertyAfterInitialization()
            {
                Chair chair = new Chair("test");
                Assert.AreEqual("test", chair.BrandName);
            }
        }
    }
}
