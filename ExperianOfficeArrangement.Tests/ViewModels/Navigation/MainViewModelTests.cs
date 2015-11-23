using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExperianOfficeArrangement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace ExperianOfficeArrangement.ViewModels.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestClass]
        public class SetStateCommandTests
        {
            [TestMethod]
            public void ShouldSetCorrectStateWithParent()
            {
                Mock<IStateViewModel> mockState = new Mock<IStateViewModel>();
                mockState.SetupAllProperties();
                MainViewModel test = new MainViewModel();
                test.SetStateCommand.Execute(mockState.Object);

                Assert.AreEqual(mockState.Object, test.CurrentState);
                Assert.AreEqual(test, mockState.Object.Parent);
            }
        }
    }
}