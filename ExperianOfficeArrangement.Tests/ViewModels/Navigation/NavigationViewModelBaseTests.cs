using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExperianOfficeArrangement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Windows.Input;

namespace ExperianOfficeArrangement.ViewModels.Tests
{
    [TestClass]
    public class NavigationViewModelBaseTests
    {
        private class TestNavigationViewModel : NavigationViewModelBase
        {
            public void SetCanTransition(bool value)
            {
                this.CanTransition = value;
            }

            public void SetNextState(IStateViewModel state)
            {
                this.nextState = state;
            }

            public override IStateViewModel GetNextState()
            {
                return this.nextState;
            }

            private IStateViewModel nextState;
        }

        [TestClass]
        public class CanTransitionTests
        {
            [TestMethod]
            public void ShouldEnableNavigationCommand()
            {
                TestNavigationViewModel test = new TestNavigationViewModel();
                bool canNavigate = false;
                test.NavigateForwardCommand.CanExecuteChanged += (s, e) => canNavigate = test.NavigateForwardCommand.CanExecute(null);
                test.PropertyChanged += (s, e) => Assert.AreEqual(PropertyName.Get<TestNavigationViewModel>(vm => vm.CanTransition), e.PropertyName);

                test.SetCanTransition(true);
                Assert.IsTrue(canNavigate);
            }
        }

        [TestClass]
        public class NavigateCommandTests
        {
            [TestMethod]
            public void ShouldSetParentState()
            {
                TestNavigationViewModel test = new TestNavigationViewModel();
                Mock<IStateViewModel> mockState = new Mock<IStateViewModel>();
                Mock<IStateContext> parentMock = new Mock<IStateContext>();
                parentMock.Setup(x => x.SetStateCommand).Returns(new Mock<ICommand>().SetupAllProperties().Object);
                test.SetNextState(mockState.Object);
                test.Parent = parentMock.Object;

                test.SetCanTransition(true);
                Assert.AreEqual(mockState.Object, test.GetNextState());

                test.NavigateForwardCommand.Execute(null);
            }
        }
    }
}