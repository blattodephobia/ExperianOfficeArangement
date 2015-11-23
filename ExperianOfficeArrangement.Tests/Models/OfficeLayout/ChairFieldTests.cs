using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models.Tests
{
    [TestClass]
    public class ChairFieldTests
    {
        [TestClass]
        public class CanHoldObjectTest
        {
            [TestMethod]
            public void ShouldHoldChairModels()
            {
                ChairField field = new ChairField();
                Assert.IsTrue(field.CanHoldObject(new Chair()));
                Assert.IsFalse(field.CanHoldObject(new Table()));
                Assert.IsFalse(field.CanHoldObject(new Flower()));
                Assert.IsFalse(field.CanHoldObject(new Pathway()));
                Assert.IsFalse(field.CanHoldObject(null));
            }
        }
    }
}