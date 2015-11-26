using ExperianOfficeArrangement.Behaviors;
using ExperianOfficeArrangement.Common;
using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExperianOfficeArrangement.ViewModels
{
    public class ArrangedFieldViewModel : BindableBase
    {
        public InteriorField PlaceHolder { get; private set; }

        public ObservableCollection<InteriorObjectBase> ArrangedItems { get; private set; }

        public IInvalidatableCommand ArrangeObjectCommand { get; private set; }

        public ArrangedFieldViewModel(InteriorField placeHolder)
        {
            this.PlaceHolder = placeHolder;
            this.ArrangedItems = new ObservableCollection<InteriorObjectBase>();
            this.ArrangeObjectCommand = new DelegateCommand(this.ArrangeObject, (param) => param is DragEventArgs ? this.CanArrangeObject(param as DragEventArgs) : this.CanArrangeObject(param as InteriorObjectBase));
        }

        private void ArrangeObject(object @object)
        {
            @object = @object is DragEventArgs ? (@object as DragEventArgs)?.Data.GetDragBehaviorData() : @object;

            if (@object is InteriorObjectBase) this.ArrangeObject(@object as InteriorObjectBase);
            else if (@object is ArrangedFieldViewModel) this.RearrangeObject(@object as ArrangedFieldViewModel);
        }

        private void ArrangeObject(InteriorObjectBase @object)
        {
            if (this.CanArrangeObject(@object))
            {
                this.ArrangedItems.Add(@object);
                this.ArrangeObjectCommand.RaiseCanExecuteChanged();
            }
        }

        private void RearrangeObject(ArrangedFieldViewModel source)
        {
            if (this.CanArrangeObject(source))
            {
                this.ArrangedItems.Add(source.ArrangedItems.First());
                source.ArrangedItems.Clear();
            }
        }

        private bool CanArrangeObject(InteriorObjectBase dragObject)
        {
            return
                this.PlaceHolder != null &&
                dragObject != null &&
                this.PlaceHolder.CanHoldObject(dragObject) &&
                !this.ArrangedItems.Any();
        }

        private bool CanArrangeObject(ArrangedFieldViewModel source)
        {
            return this.CanArrangeObject(source.ArrangedItems.FirstOrDefault());
        }

        private bool CanArrangeObject(DragEventArgs dragArgs)
        {
            object data = DragDropBehavior.GetDragBehaviorData(dragArgs.Data);
            if (data is InteriorObjectBase) return this.CanArrangeObject(data as InteriorObjectBase);
            else if (data is ArrangedFieldViewModel) return this.CanArrangeObject(data as ArrangedFieldViewModel);
            else return false;
        }
    }
}
