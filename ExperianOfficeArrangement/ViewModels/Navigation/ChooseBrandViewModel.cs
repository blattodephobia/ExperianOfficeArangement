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
        private string brandName;
        public string BrandName
        {
            get
            {
                return this.brandName;
            }

            set
            {
                this.SetProperty(ref this.brandName, value);
                this.CanTransition = !string.IsNullOrEmpty(this.brandName);
            }
        }

        public ObservableCollection<string> Brands { get; private set; }

        public override IStateViewModel GetNextState()
        {
            ArrangementPageViewModel result = new ArrangementPageViewModel(this.layout, new Chair(this.BrandName), new Table(this.BrandName));
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
