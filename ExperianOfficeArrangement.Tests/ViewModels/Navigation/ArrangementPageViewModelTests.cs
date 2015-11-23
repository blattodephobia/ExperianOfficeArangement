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
    [TestClass]
    public class ArrangementPageViewModelTests
    {
        private static readonly InteriorField[,] Layout = new InteriorField[2, 3]
        {
            { new ChairField(), new TableField(), new FlowerField() },
            { new EmptySpaceField(), new PathwayField(), new PathwayField() },
        };

        [TestClass]
        public class CtorTests
        {
            [TestMethod]
            public void ShouldInitializeModelsCorrectly()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);
                Assert.AreEqual(chairModel, vm.ChairModel);
                Assert.AreEqual(tableModel, vm.TableModel);
            }

            [TestMethod]
            public void ShouldInitializeDimensionsCorrectly()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                Assert.AreEqual(2, vm.Rows);
                Assert.AreEqual(3, vm.Columns);
            }

            [TestMethod]
            public void ShouldInitializeNonFunitureCorrectly()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                Assert.IsTrue(vm.Arrangement
                    .Where(arrangement => !((arrangement.PlaceHolder is FurnitureField) || (arrangement.PlaceHolder is EmptySpaceField)))
                    .All(arrangement => arrangement.ArrangedItems.Any()));
            }
        }

        [TestClass]
        public class ClearAllCommandTests
        {
            [TestMethod]
            public void ShouldClearArrangedModels()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);
                vm.Arrangement[0].ArrangeObjectCommand.Execute(chairModel);
                vm.Arrangement[1].ArrangeObjectCommand.Execute(tableModel);

                Assert.IsTrue(vm.ClearAllCommand.CanExecute(null));
                vm.ClearAllCommand.Execute(null);
                Assert.AreEqual(null, vm.Arrangement[0].ArrangedItems.FirstOrDefault());
                Assert.AreEqual(null, vm.Arrangement[1].ArrangedItems.FirstOrDefault());
            }
        }
    }
}