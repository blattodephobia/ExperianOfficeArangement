using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExperianOfficeArrangement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperianOfficeArrangement.Factories;
using Moq;
using ExperianOfficeArrangement.Models;

namespace ExperianOfficeArrangement.ViewModels.Tests
{
    [TestClass]
    public class LoadMapViewModelTests
    {
        static LoadMapViewModelTests()
        {
            MockFactory = new Mock<IInteriorLayoutFactory>();
            MockFactory.Setup(x => x.GetLayout()).Returns(new InteriorField[0, 0]);
            MockFactory.SetupGet(x => x.LayoutIdentifier).Returns("test");
        }

        private static Mock<IInteriorLayoutFactory> MockFactory;

        [TestClass]
        public class LoadLayoutCommandTests
        {
            [TestMethod]
            public void ShouldSetIdentifier()
            {
                LoadMapViewModel vm = new LoadMapViewModel(MockFactory.Object);
                Assert.IsTrue(vm.LoadMapCommand.CanExecute(null));
                vm.LoadMapCommand.Execute(null);
                Assert.AreEqual("test", vm.LayoutIdentifier);
            }
        }

        [TestClass]
        public class GetNextStateTests
        {
            [TestMethod]
            public void ShouldTransitionToChooseBrand()
            {
                LoadMapViewModel vm = new LoadMapViewModel(MockFactory.Object);
                Assert.IsFalse(vm.CanTransition);
                vm.LoadMapCommand.Execute(null);
                Assert.IsTrue(vm.CanTransition);
                Assert.IsTrue(vm.GetNextState() is ChooseBrandViewModel);
            }

            [TestMethod]
            public void ShouldReturnNullIfNoLayout()
            {
                LoadMapViewModel vm = new LoadMapViewModel(MockFactory.Object);
                Assert.IsFalse(vm.CanTransition);
                Assert.AreEqual(null, vm.GetNextState());
            }
        }
    }
}