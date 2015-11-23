using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperianOfficeArrangement.Common
{
    [TestClass]
    public class BindableBaseTests
    {
        [TestClass]
        public class PropertyChangedTests
        {
            private class MockBindable : BindableBase
            {
                private string mockProperty;
                public string MockProperty
                {
                    get
                    {
                        return this.mockProperty;
                    }

                    set
                    {
                        this.SetProperty(ref this.mockProperty, value);
                    }
                }

                public void ChangePropertyViaExpressionNameGetter()
                {
                    this.OnPropertyChanged(() => this.MockProperty);
                }
            }

            [TestMethod]
            public void ShouldRaisePropertyChangedViaSetter()
            {
                MockBindable bindable = new MockBindable();
                int invokationsCount = 0;
                bindable.PropertyChanged += (s, e) =>
                {
                    Assert.AreEqual("MockProperty", e.PropertyName);
                    invokationsCount++;
                };

                bindable.MockProperty = "";
                Assert.AreEqual(1, invokationsCount);
            }

            [TestMethod]
            public void ShouldRaisPropertyChangedViaExpressionNameGetter()
            {
                MockBindable bindable = new MockBindable();
                int invokationsCount = 0;
                bindable.PropertyChanged += (s, e) =>
                {
                    Assert.AreEqual("MockProperty", e.PropertyName);
                    invokationsCount++;
                };

                bindable.ChangePropertyViaExpressionNameGetter();
                Assert.AreEqual(1, invokationsCount);
            }
        }
    }
}
