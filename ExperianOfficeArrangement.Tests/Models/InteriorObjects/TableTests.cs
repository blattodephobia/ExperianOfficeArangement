using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    [TestClass]
    class TableTests
    {
        [TestClass]
        public class CtorTests
        {
            [TestMethod]
            public void ShouldSetPropertyAfterInitialization()
            {
                Table table = new Table("test");
                Assert.AreEqual("test", table.BrandName);
            }
        }
    }
}
