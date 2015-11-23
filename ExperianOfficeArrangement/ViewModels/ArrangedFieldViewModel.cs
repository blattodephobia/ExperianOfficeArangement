using ExperianOfficeArrangement.Common;
using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.ViewModels
{
    public class ArrangedFieldViewModel : BindableBase
    {
        public static readonly string DataFormat = "exp_ArrangedFieldViewModel";

        public InteriorField PlaceHolder { get; private set; }

        public ObservableCollection<InteriorObjectBase> ArrangedItems { get; private set; }

        public IInvalidatableCommand ArrangeObjectCommand { get; private set; }

        public ArrangedFieldViewModel(InteriorField placeHolder)
        {
            this.PlaceHolder = placeHolder;
            this.ArrangedItems = new ObservableCollection<InteriorObjectBase>();
            this.ArrangeObjectCommand = new DelegateCommand(this.ArrangeObject, this.CanArrangeObject);
        }

        private void ArrangeObject(object @object)
        {
            if (this.CanArrangeObject(@object))
            {
                this.ArrangedItems.Add(@object as InteriorObjectBase);
                this.ArrangeObjectCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanArrangeObject(object @object)
        {
            return
                this.PlaceHolder != null &&
                (@object is InteriorObjectBase) &&
                this.PlaceHolder.CanHoldObject(@object as InteriorObjectBase) &&
                !this.ArrangedItems.Any();
        }
    }
}
