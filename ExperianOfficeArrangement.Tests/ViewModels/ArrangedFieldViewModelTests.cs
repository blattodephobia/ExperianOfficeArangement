using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExperianOfficeArrangement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperianOfficeArrangement.Models;

namespace ExperianOfficeArrangement.ViewModels.Tests
{
    [TestClass()]
    public class ArrangedFieldViewModelTests
    {
        [TestClass]
        public class CtorTests
        {
            [TestMethod]
            public void ShouldInitializeCorrectly()
            {
                ChairField testChair = new ChairField();
                ArrangedFieldViewModel vm = new ArrangedFieldViewModel(testChair);
                Assert.AreEqual(testChair, vm.PlaceHolder);
            }
        }

        [TestClass]
        public class ArrangeObjectCommandTests
        {
            [TestMethod]
            public void ShouldBeEnabledForEmptyArrangementAndMatchingObject()
            {
                ChairField testChair = new ChairField();
                ArrangedFieldViewModel vm = new ArrangedFieldViewModel(testChair);
                Assert.IsTrue(vm.ArrangeObjectCommand.CanExecute(new Chair()));
            }

            [TestMethod]
            public void ShouldBeDisabledForEmptyArrangementAndMismatchedObject()
            {
                ChairField testChair = new ChairField();
                ArrangedFieldViewModel vm = new ArrangedFieldViewModel(testChair);
                Assert.IsFalse(vm.ArrangeObjectCommand.CanExecute(new Table()));
            }

            [TestMethod]
            public void ShouldBeDisabledForFullArrangement()
            {
                ChairField testChair = new ChairField();
                ArrangedFieldViewModel vm = new ArrangedFieldViewModel(testChair);
                vm.ArrangeObjectCommand.Execute(new Chair());
                Assert.IsFalse(vm.ArrangeObjectCommand.CanExecute(new Chair()));
            }

            [TestMethod]
            public void ShouldArrangeCorrectly()
            {
                ChairField testChair = new ChairField();
                ArrangedFieldViewModel vm = new ArrangedFieldViewModel(testChair);
                Chair obj = new Chair();
                vm.ArrangeObjectCommand.Execute(obj);
                Assert.AreEqual(obj, vm.ArrangedItems.First());
            }
        }
    }
}