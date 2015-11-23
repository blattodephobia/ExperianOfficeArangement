using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            this.OnPropertyChanged(PropertyName.Get(propertySelector));
        }
        
        protected void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string callingMember = "")
        {
            backingField = value;
            this.OnPropertyChanged(callingMember);
        }
    }
}
