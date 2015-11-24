using ExperianOfficeArrangement.Common;
using ExperianOfficeArrangement.Factories;
using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.ViewModels
{
    public class ArrangementPageViewModel : NavigationViewModelBase
    {
        public static readonly string DataFormat = "exp_ArrangementPageViewModel";

        private ObservableCollection<ArrangedFieldViewModel> arrangement;
        public ObservableCollection<ArrangedFieldViewModel> Arrangement
        {
            get
            {
                return this.arrangement;
            }

            private set
            {
                this.SetProperty(ref this.arrangement, value);
            }
        }

        private int rows;
        public int Rows
        {
            get
            {
                return this.rows;
            }

            set
            {
                this.SetProperty(ref this.rows, value);
            }
        }

        private int columns;
        public int Columns
        {
            get
            {
                return this.columns;
            }

            set
            {
                this.SetProperty(ref this.columns, value);
            }
        }

        private Table tableModel;
        public Table TableModel
        {
            get
            {
                return this.tableModel;
            }

            set
            {
                this.SetProperty(ref this.tableModel, value);
            }
        }

        private Chair chairModel;
        public Chair ChairModel
        {
            get
            {
                return this.chairModel;
            }

            set
            {
                this.SetProperty(ref this.chairModel, value);
            }
        }

        private string selectedPalette;
        public string SelectedPalette
        {
            get
            {
                return this.selectedPalette;
            }

            set
            {
                this.SetProperty(ref this.selectedPalette, value);
            }
        }

        public IInvalidatableCommand ClearAllCommand { get; private set; }

        public IInvalidatableCommand FillAllCommand { get; private set; }

        public ObservableCollection<string> Palettes { get; private set; }

        public ArrangementPageViewModel(InteriorField[,] layout, Chair chairModel, Table tableModel)
        {
            this.Rows = layout.GetLength(0);
            this.Columns = layout.GetLength(1);
            this.Arrangement = new ObservableCollection<ArrangedFieldViewModel>();
            for (int y = 0; y < layout.GetLength(0); y++)
                for (int x = 0; x < layout.GetLength(1); x++)
                {
                    this.Arrangement.Add(new ArrangedFieldViewModel((layout[y, x])));
                }

            this.Palettes = new ObservableCollection<string>(new[] { "Light", "Dark" });
            this.ChairModel = chairModel;
            this.TableModel = tableModel;

            Pathway autoPathway = new Pathway();
            Flower autoFlower = new Flower();
            foreach (ArrangedFieldViewModel field in this.Arrangement)
            {
                if (field?.PlaceHolder?.CanHoldObject(autoFlower) ?? false) field.ArrangeObjectCommand.Execute(autoFlower);
                else if (field?.PlaceHolder?.CanHoldObject(autoPathway) ?? false) field.ArrangeObjectCommand.Execute(autoPathway);
            }

            this.ClearAllCommand = new DelegateCommand((param) => this.ClearAll());
            this.FillAllCommand = new DelegateCommand((param) => this.FillAll());
        }

        private void ClearAll()
        {
            foreach (ArrangedFieldViewModel vm in this.Arrangement)
            {
                if (vm.ArrangedItems.FirstOrDefault() is FurnitureItem)
                {
                    vm.ArrangedItems.Clear();
                }
            }
        }

        private void FillAll()
        {
            foreach (ArrangedFieldViewModel field in this.Arrangement.Where(a => (a.PlaceHolder is FurnitureField) && !a.ArrangedItems.Any()))
            {
                FurnitureItem targetObject = (field.PlaceHolder is ChairField) ? new Chair() as FurnitureItem : new Table() as FurnitureItem;
                field.ArrangeObjectCommand.Execute(targetObject);
            }
        }
    }
}
