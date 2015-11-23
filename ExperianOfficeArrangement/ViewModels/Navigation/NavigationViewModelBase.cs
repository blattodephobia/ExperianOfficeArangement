using ExperianOfficeArrangement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExperianOfficeArrangement.ViewModels
{
    public abstract class NavigationViewModelBase : BindableBase, IStateViewModel
    {
        private IStateContext parent;

        private bool canTransition;
        public virtual bool CanTransition
        {
            get
            {
                return this.canTransition;
            }

            protected set
            {
                this.SetProperty(ref this.canTransition, value);
                (this.NavigateForwardCommand as IInvalidatableCommand)?.RaiseCanExecuteChanged();
            }
        }

        public IStateContext Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                if (value == null) throw new InvalidOperationException($"{nameof(this.Parent)} cannot be null.");
                this.SetProperty(ref this.parent, value);
            }
        }

        public ICommand NavigateForwardCommand { get; private set; }

        public virtual IStateViewModel GetNextState()
        {
            return null;
        }

        public NavigationViewModelBase()
        {
            this.NavigateForwardCommand = new DelegateCommand(this.OnNavigateForward, (param) => this.CanTransition);
        }

        protected virtual void OnNavigateForward(object param)
        {
            if (this.CanTransition && this.Parent.SetStateCommand.CanExecute(param))
            {
                this.Parent.SetStateCommand.Execute(this.GetNextState());
            }
        }
    }
}
