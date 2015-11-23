using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExperianOfficeArrangement.Common
{
    public interface IInvalidatableCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
