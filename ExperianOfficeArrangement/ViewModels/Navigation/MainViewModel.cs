using ExperianOfficeArrangement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExperianOfficeArrangement.ViewModels
{
    public class MainViewModel : BindableBase, IStateContext
    {
        private IStateViewModel currentState;
        public IStateViewModel CurrentState
        {
            get
            {
                return this.currentState;
            }

            private set
            {
                this.SetProperty(ref this.currentState, value);
            }
        }

        public ICommand SetStateCommand { get; private set; }

        public MainViewModel()
        {
            this.CurrentState = new LoadMapViewModel(new Factories.FileLoadInteriorLayoutFactory()) { Parent = this };
            this.SetStateCommand = new DelegateCommand(this.SetState);
        }

        private void SetState(object state)
        {
            this.CurrentState = state as IStateViewModel;
            this.CurrentState.Parent = this;
        }
    }
}
