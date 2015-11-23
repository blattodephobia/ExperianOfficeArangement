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
    public class ChooseBrandViewModelTests
    {
        [TestClass]
        public class GetNextStateTests
        {
            [TestMethod]
            public void ShoudlSetBrand()
            {
                InteriorField[,] layout = new InteriorField[0, 0];
                ChooseBrandViewModel test = new ChooseBrandViewModel(layout);
                test.SelectedBrand = "test";
                ArrangementPageViewModel nextState = test.GetNextState() as ArrangementPageViewModel;
                Assert.AreEqual("test", nextState.TableModel.BrandName);
                Assert.AreEqual("test", nextState.ChairModel.BrandName);
            }
        }
    }
}