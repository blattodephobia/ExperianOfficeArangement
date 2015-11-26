using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExperianOfficeArrangement.Behaviors
{
    public static class DragDropBehavior
    {
        private static readonly string DragDropBehaviorFormat = nameof(DragDropBehaviorFormat);

        public static readonly DependencyProperty DragObjectProperty =
            DependencyProperty.RegisterAttached(PropertyName.FromDependencyProperty(() => DragObjectProperty), typeof(object), typeof(DragDropBehavior), new PropertyMetadata(DragObjectPropertyChanged));

        public static readonly DependencyProperty DragEffectProperty =
            DependencyProperty.RegisterAttached(PropertyName.FromDependencyProperty(() => DragEffectProperty), typeof(DragDropEffects), typeof(DragDropBehavior), new PropertyMetadata(DragDropEffects.All));

        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached(PropertyName.FromDependencyProperty(() => DropCommandProperty), typeof(ICommand), typeof(DragDropBehavior), new PropertyMetadata(DropCommandPropertyChanged));

        public static ICommand GetDropCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DropCommandProperty);
        }

        public static void SetDropCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DropCommandProperty, value);
        }

        public static object GetDragObject(DependencyObject obj)
        {
            return obj.GetValue(DragObjectProperty);
        }

        public static void SetDragObject(DependencyObject obj, object value)
        {
            obj.SetValue(DragObjectProperty, value);
        }

        public static DragDropEffects GetDragEffect(DependencyObject obj)
        {
            return (DragDropEffects)obj.GetValue(DragEffectProperty);
        }

        public static object GetDragBehaviorData(this IDataObject dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException(nameof(dataObject));

            return dataObject.GetData(DragDropBehaviorFormat);
        }

        public static void SetDragEffect(DependencyObject obj, DragDropEffects value)
        {
            obj.SetValue(DragEffectProperty, value);
        }

        private static void DragDropBehavior_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DependencyObject _sender = sender as DependencyObject;
                DataObject data = new DataObject(DragDropBehaviorFormat, _sender.GetValue(DragObjectProperty));
                DragDrop.DoDragDrop(_sender, data, (DragDropEffects)_sender.GetValue(DragEffectProperty));
            }
        }

        private static void DragObjectPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            UIElement _source = source as UIElement;
            if (_source == null) return;

            if (args.OldValue == null && args.NewValue != null)
            {
                _source.MouseMove += DragDropBehavior_MouseMove;
            }
            else if (args.OldValue != null && args.NewValue == null)
            {
                _source.MouseMove -= DragDropBehavior_MouseMove;
            }
        }

        private static void DropCommandPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            UIElement target = source as UIElement;
            if (target == null) return;

            if (args.OldValue == null && args.NewValue != null)
            {
                target.DragEnter += TargetDragEnter;
                target.DragOver += TargetDragOver;
                target.Drop += TargetDrop;
                target.AllowDrop = true;
            }
            else if (args.NewValue != null && args.OldValue == null)
            {
                target.DragEnter -= TargetDragEnter;
                target.DragOver -= TargetDragOver;
                target.Drop -= TargetDrop;
                target.AllowDrop = false;
            }
        }

        private static void TargetDragEnter(object sender, DragEventArgs e)
        {
            ValidateDragAction(sender, e);
        }

        private static void TargetDragOver(object sender, DragEventArgs e)
        {
            ValidateDragAction(sender, e);
        }

        private static void ValidateDragAction(object sender, DragEventArgs e)
        {
            ICommand targetCommand = GetDropCommand(sender as DependencyObject);
            if (targetCommand != null)
            {
                try
                {
                    if (!targetCommand.CanExecute(e)) e.Effects = DragDropEffects.None;
                }
                catch
                {
                    e.Effects = DragDropEffects.None;
                }

                e.Handled = true;
            }
        }

        private static void TargetDrop(object sender, DragEventArgs e)
        {
            ICommand targetCommand = GetDropCommand(sender as DependencyObject);
            if (targetCommand != null)
            {
                try
                {
                    if (targetCommand.CanExecute(e))
                    {
                        targetCommand.Execute(e);
                    }
                }
                catch
                {

                }
            }
        }        
    }
}
