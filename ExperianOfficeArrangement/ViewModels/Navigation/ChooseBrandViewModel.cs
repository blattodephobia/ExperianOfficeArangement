using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.ViewModels
{
    public class ChooseBrandViewModel : NavigationViewModelBase
    {
        private string selectedBrandName;
        public string SelectedBrand
        {
            get
            {
                return this.selectedBrandName;
            }

            set
            {
                this.SetProperty(ref this.selectedBrandName, value);
                this.CanTransition = !string.IsNullOrEmpty(this.selectedBrandName);
            }
        }

        public ObservableCollection<string> Brands { get; private set; }

        public override IStateViewModel GetNextState()
        {
            ArrangementPageViewModel result = new ArrangementPageViewModel(this.layout, new Chair(this.SelectedBrand), new Table(this.SelectedBrand));
            return result;
        }

        public ChooseBrandViewModel(InteriorField[,] layout)
        {
            this.layout = layout;
            this.Brands = new ObservableCollection<string>(new string[] { "A", "B" });
        }

        private InteriorField[,] layout;
    }
}
