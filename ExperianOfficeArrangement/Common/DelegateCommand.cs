using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExperianOfficeArrangement.Common
{
    public class DelegateCommand : IInvalidatableCommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecuteCallback?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            this._action.Invoke(parameter);
        }
        
        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public DelegateCommand(Action<object> action, Func<object, bool> canExecuteCallback = null)
        {
            if (action == null) throw new ArgumentNullException("action");

            this._action = action;
            this.canExecuteCallback = canExecuteCallback;
        }

        private Action<object> _action;
        private Func<object, bool> canExecuteCallback;
    }
}
