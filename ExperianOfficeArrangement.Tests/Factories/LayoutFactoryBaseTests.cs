using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExperianOfficeArrangement.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperianOfficeArrangement.Models;

namespace ExperianOfficeArrangement.Factories.Tests
{
    [TestClass]
    public class LayoutFactoryBaseTests
    {
        private class TestLayoutFactory : LayoutFactoryBase
        {
            public override InteriorField[,] GetLayout()
            {
                return null;
            }

            public InteriorField CreateFieldPublic(char symbol)
            {
                return this.CreateField(symbol);
            }
        }

        [TestClass]
        public class CreateFieldTests
        {
            [TestMethod]
            public void ShouldInstantiateCorrectObjects()
            {
                TestLayoutFactory testFactory = new TestLayoutFactory();

                Assert.IsTrue(testFactory.CreateFieldPublic(EmptySpaceField.IDENTIFIER) is EmptySpaceField);
                Assert.IsTrue(testFactory.CreateFieldPublic(ChairField.IDENTIFIER) is ChairField);
                Assert.IsTrue(testFactory.CreateFieldPublic(TableField.IDENTIFIER) is TableField);
                Assert.IsTrue(testFactory.CreateFieldPublic(FlowerField.IDENTIFIER) is FlowerField);
                Assert.IsTrue(testFactory.CreateFieldPublic(PathwayField.IDENTIFIER) is PathwayField);
            }

            [TestMethod]
            public void ShouldReturnNullIfNoObjectFound()
            {
                TestLayoutFactory testFactory = new TestLayoutFactory();

                Assert.AreEqual(null, testFactory.CreateFieldPublic('r'));
            }
        }
    }
}
