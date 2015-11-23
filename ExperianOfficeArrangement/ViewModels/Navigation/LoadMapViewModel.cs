using ExperianOfficeArrangement.Common;
using ExperianOfficeArrangement.Factories;
using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.ViewModels
{
    public class LoadMapViewModel : NavigationViewModelBase
    {
        public LoadMapViewModel() :
            this(new RandomLayoutFactory(0))
        {
        }

        public LoadMapViewModel(IInteriorLayoutFactory layoutFactory)
        {
            this.layoutFactory = layoutFactory;
            this.LoadMapCommand = new DelegateCommand(this.LoadLayout);
        }

        private string layoutIdentifier;
        public string LayoutIdentifier
        {
            get
            {
                return this.layoutIdentifier;
            }

            set
            {
                this.SetProperty(ref this.layoutIdentifier, value);
            }
        }

        public IInvalidatableCommand LoadMapCommand { get; private set; }

        public override IStateViewModel GetNextState()
        {
            return this.layout != null ? new ChooseBrandViewModel(this.layout) : null;
        }

        private void LoadLayout(object dummyParam)
        {
            this.layout = this.layoutFactory.GetLayout();
            if (this.layout != null)
            {
                this.LayoutIdentifier = layoutFactory.LayoutIdentifier;
                this.CanTransition = this.layout != null;
            }
        }

        private InteriorField[,] layout;
        private IInteriorLayoutFactory layoutFactory;
    }
}
