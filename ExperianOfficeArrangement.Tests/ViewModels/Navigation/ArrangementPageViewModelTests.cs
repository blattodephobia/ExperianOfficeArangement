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

        [TestClass]
        public class FillAllCommandTests
        {
            [TestMethod]
            public void ShouldFillAllEmptyFields1()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                Assert.AreEqual(null, vm.Arrangement[0].ArrangedItems.FirstOrDefault());
                Assert.AreEqual(null, vm.Arrangement[1].ArrangedItems.FirstOrDefault());

                Assert.IsTrue(vm.FillAllCommand.CanExecute(null));
                vm.FillAllCommand.Execute(null);

                Assert.IsTrue(vm.Arrangement[0].ArrangedItems.First() is Chair);
                Assert.IsTrue(vm.Arrangement[1].ArrangedItems.First() is Table);
            }

            [TestMethod]
            public void ShouldFillAllEmptyFields2()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                Assert.AreEqual(null, vm.Arrangement[0].ArrangedItems.FirstOrDefault());
                Assert.AreEqual(null, vm.Arrangement[1].ArrangedItems.FirstOrDefault());

                Assert.IsTrue(vm.FillAllCommand.CanExecute(null));
                vm.FillAllCommand.Execute(null);

                Assert.IsTrue(vm.Arrangement[0].ArrangedItems.First() is Chair);
                Assert.IsTrue(vm.Arrangement[1].ArrangedItems.First() is Table);

                Assert.IsFalse(vm.Arrangement.Any(a => !(a.PlaceHolder is FurnitureField) && a.ArrangedItems.Any(item => item is FurnitureItem)));
            }

            [TestMethod]
            public void ShouldNotReplaceFilledFields()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                vm.Arrangement[0].ArrangeObjectCommand.Execute(chairModel);
                vm.FillAllCommand.Execute(null);

                Assert.AreEqual(chairModel, vm.Arrangement[0].ArrangedItems.First());
                Assert.IsTrue(vm.Arrangement[1].ArrangedItems.First() is Table);
                Assert.IsFalse(vm.Arrangement[1].ArrangedItems.First() == tableModel);
            }
        }

        [TestClass]
        public class RemoveObjectCommandTests
        {
            [TestMethod]
            public void ShouldRemoveObject()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                vm.Arrangement[0].ArrangeObjectCommand.Execute(chairModel);
                Assert.AreEqual(chairModel, vm.Arrangement[0].ArrangedItems.First());

                Assert.IsTrue(vm.RemoveObjectCommand.CanExecute(vm.Arrangement[0]));
                vm.RemoveObjectCommand.Execute(vm.Arrangement[0]);
                Assert.AreEqual(null, vm.Arrangement[0].ArrangedItems.FirstOrDefault());
            }

            [TestMethod]
            public void ShouldBeDisabledForNonFurniture1()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                ArrangedFieldViewModel field = new ArrangedFieldViewModel(new FlowerField());
                field.ArrangeObjectCommand.Execute(new Flower());
                Assert.IsFalse(vm.RemoveObjectCommand.CanExecute(field));
            }

            [TestMethod]
            public void ShouldBeDisabledForNonFurniture2()
            {
                Chair chairModel = new Chair();
                Table tableModel = new Table();
                ArrangementPageViewModel vm = new ArrangementPageViewModel(Layout, chairModel, tableModel);

                ArrangedFieldViewModel field = new ArrangedFieldViewModel(new FlowerField());
                field.ArrangeObjectCommand.Execute(new Pathway());
                Assert.IsFalse(vm.RemoveObjectCommand.CanExecute(field));
            }
        }
    }
}