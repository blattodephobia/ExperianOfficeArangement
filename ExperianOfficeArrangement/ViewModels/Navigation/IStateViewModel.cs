using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExperianOfficeArrangement.ViewModels
{
    public interface IStateViewModel : INotifyPropertyChanged
    {
        IStateViewModel GetNextState();

        IStateContext Parent { get; set; }

        bool CanTransition { get; }

        ICommand NavigateForwardCommand { get; }
    }
}
