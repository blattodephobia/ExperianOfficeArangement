using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExperianOfficeArrangement.ViewModels
{
    public interface IStateContext : INotifyPropertyChanged
    {
        ICommand SetStateCommand { get; }

        IStateViewModel CurrentState { get; }
    }
}
