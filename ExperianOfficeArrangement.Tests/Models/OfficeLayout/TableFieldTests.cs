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
    public class TableFieldTests
    {
        [TestClass]
        public class CanHoldObjectTests
        {
            [TestMethod]
            public void ShouldHoldTables()
            {
                TableField tableField = new TableField();
                Assert.IsTrue(tableField.CanHoldObject(new Table()));
                Assert.IsFalse(tableField.CanHoldObject(new Chair()));
                Assert.IsFalse(tableField.CanHoldObject(new Flower()));
                Assert.IsFalse(tableField.CanHoldObject(new Pathway()));
                Assert.IsFalse(tableField.CanHoldObject(null));
            }
        }
    }
}