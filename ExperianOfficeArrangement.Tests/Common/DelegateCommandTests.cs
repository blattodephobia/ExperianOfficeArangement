using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Common
{
    [TestClass]
    public class DelegateCommandTests
    {
        [TestClass]
        public class CanExecuteChangedTests
        {
            [TestMethod]
            public void ShouldRaiseCanExecuteChanged()
            {
                DelegateCommand testCommand = new DelegateCommand((param) => { });
                int invokationsCount = 0;
                testCommand.CanExecuteChanged += (s, e) => invokationsCount++;
                testCommand.RaiseCanExecuteChanged();
                Assert.AreEqual(1, invokationsCount);
            }
        }

        [TestClass]
        public class ExecuteTests
        {
            [TestMethod]
            public void ShouldExecuteCorrectMethod()
            {
                bool methodCalled = false;
                DelegateCommand testCommand = new DelegateCommand((param) => methodCalled = true);

                testCommand.Execute(null);
                Assert.IsTrue(methodCalled);
            }
        }

        [TestClass]
        public class CanExecuteTests
        {
            [TestMethod]
            public void ShouldReturnTrueIfConstructedWithNull()
            {
                bool methodCalled = false;
                DelegateCommand testCommand = new DelegateCommand((param) => methodCalled = true);

                testCommand.Execute(null);
                Assert.IsTrue(methodCalled);

                Assert.IsTrue(testCommand.CanExecute(null));
            }

            [TestMethod]
            public void ShouldReturnResultFromCorrectCanExecuteDelegate()
            {
                bool methodCalled = false;
                bool canExecuteCalled = false;
                DelegateCommand testCommand = new DelegateCommand((param) => methodCalled = true, (param) =>
                {
                    canExecuteCalled = true;
                    return true;
                });

                testCommand.Execute(null);
                Assert.IsTrue(methodCalled);
                Assert.IsFalse(canExecuteCalled);
                Assert.IsTrue(testCommand.CanExecute(null));
                Assert.IsTrue(canExecuteCalled);
            }
        }

        [TestClass]
        public class CtorTests
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ShouldThrowOnNullActionCallback()
            {
                DelegateCommand command = new DelegateCommand(null);
            }
        }
    }
}
