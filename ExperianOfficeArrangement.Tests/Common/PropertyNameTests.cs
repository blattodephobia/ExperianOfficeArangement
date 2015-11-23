using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperianOfficeArrangement
{
    [TestClass]
    public class PropertyNameTests
    {
        [TestClass]
        public class NameOfTests
        {
            static string StaticProperty { get; set; }

            const int Constant = 0;

            string TestProperty { get; set; }

            string testField = null;

            int TestValueTypeProperty { get; set; }

            [TestMethod]
            public void ShouldReturnInstanceMemberName()
            {
                string actual = PropertyName.Get(() => this.TestProperty);
                Assert.AreEqual("TestProperty", actual);
            }

            [TestMethod]
            public void ShouldReturnInstanceMemberNameWithBoxing()
            {
                string actual = PropertyName.Get<object>(() => this.TestValueTypeProperty);
                Assert.AreEqual("TestValueTypeProperty", actual);
            }

            [TestMethod]
            public void ShouldReturnInstanceFieldName()
            {
                string actual = PropertyName.Get(() => this.testField);
                Assert.AreEqual("testField", actual);
            }

            [TestMethod]
            public void ShouldReturnNonInstantiatedMemberName()
            {
                string actual = PropertyName.Get<NameOfTests>((obj) => obj.TestProperty);
                Assert.AreEqual("TestProperty", actual);
            }

            [TestMethod]
            public void ShouldReturnNonInstantiatedMemberNameWithBoxing()
            {
                string actual = PropertyName.Get<NameOfTests>((obj) => obj.TestValueTypeProperty);
                Assert.AreEqual("TestValueTypeProperty", actual);
            }

            [TestMethod]
            public void ShouldReturnNonInstantiatedFieldName()
            {
                string actual = PropertyName.Get<NameOfTests>((obj) => obj.testField);
                Assert.AreEqual("testField", actual);
            }

            [TestMethod]
            public void ShouldReturnStaticPropertyName()
            {
                string actual = PropertyName.Get(() => StaticProperty);
                Assert.AreEqual("StaticProperty", actual);
                actual = PropertyName.Get(() => NameOfTests.StaticProperty);
                Assert.AreEqual("StaticProperty", actual);
            }

            [TestMethod]
            public void ShouldReturnDependencyPropertyName()
            {
                DependencyProperty testProperty = null;
                string actual = PropertyName.FromDependencyProperty(() => testProperty);
                Assert.AreEqual("test", actual);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public void ShouldThrowOnConstantExpression()
            {
                string actual = PropertyName.Get(() => Constant);
                Assert.AreEqual("Constant", actual);
                actual = PropertyName.Get(() => NameOfTests.Constant);
                Assert.AreEqual("Constant", actual);
            }
        }
    }
}
